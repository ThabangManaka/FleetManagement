using Fleet.Application.Features.FleetDashboard.DTOs;
using Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard;
using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Core.Enums;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Tests.Features.FleetDashboard
{
    public class GetFleetDashboardQueryHandlerTests
    {

        private readonly Mock<IVehicleRepository> _vehicleRepository;
        private readonly Mock<IDriverRepository> _driverRepository;
        private readonly Mock<IFuelTransactionRepository> _fuelRepository;
        private readonly Mock<IMaintenanceRepository> _maintenanceRepository;
        private readonly Mock<ITripRepository> _tripRepository;

        private readonly GetFleetDashboardQueryHandler _handler;

        public GetFleetDashboardQueryHandlerTests()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _driverRepository = new Mock<IDriverRepository>();
            _fuelRepository = new Mock<IFuelTransactionRepository>();
            _maintenanceRepository = new Mock<IMaintenanceRepository>();
            _tripRepository = new Mock<ITripRepository>();

            _handler = new GetFleetDashboardQueryHandler(
                _vehicleRepository.Object,
                _driverRepository.Object,
                _fuelRepository.Object,
                _maintenanceRepository.Object,
                _tripRepository.Object);
        }
            [Fact]
            public async Task Handle_Should_Return_Total_Vehicle_Count()
            {
                // Arrange
                _vehicleRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Vehicle>
                    {
                      new Vehicle(
                        "TEST001",
                        "VIN001",
                        "Toyota",
                        "Hilux",
                        2025,
                        FuelType.Diesel),

                    new Vehicle(
                        "TEST002",
                        "VIN002",
                        "Ford",
                        "Ranger",
                        2026,
                        FuelType.Diesel)
                    });

                _driverRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Driver>());

                _fuelRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<FuelTransaction>());

                _maintenanceRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Maintenance>());

                _tripRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Trip>());

                var query = new GetFleetDashboardQuery(null, null);

                // Act
                var result = await _handler.Handle(
                    query,
                    CancellationToken.None);

                // Assert
                result.TotalVehicles.Should().Be(2);
            }

            [Fact]
            public async Task Handle_Should_Return_Correct_Vehicle_Status_Counts()
            {
                // Arrange
                var availableVehicle = new Vehicle(
                    "TEST001",
                    "VIN001",
                    "Toyota",
                    "Hilux",
                    2025,
                    FuelType.Diesel);

                var assignedVehicle = new Vehicle(
                    "TEST002",
                    "VIN002",
                    "Ford",
                    "Ranger",
                    2026,
                    FuelType.Diesel);

                var maintenanceVehicle = new Vehicle(
                    "TEST003",
                    "VIN003",
                    "Isuzu",
                    "D-Max",
                    2025,
                    FuelType.Diesel);

            assignedVehicle.UpdateDetails(
                     "TEST002",
                     "VIN002",
                     "Ford",
                     "Ranger",
                     2026,
                     FuelType.Diesel,
                     VehicleStatus.Assigned,
                     0);

            maintenanceVehicle.UpdateDetails(
                    "TEST003",
                    "VIN003",
                    "Isuzu",
                    "D-Max",
                    2025,
                    FuelType.Diesel,
                    VehicleStatus.Maintenance,
                    0);

            _vehicleRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Vehicle>
                    {
                        availableVehicle,
                        assignedVehicle,
                        maintenanceVehicle
                    });

                _driverRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Driver>());

                _fuelRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<FuelTransaction>());

                _maintenanceRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Maintenance>());

                _tripRepository
                    .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Trip>());

                var query = new GetFleetDashboardQuery(null, null);

                // Act
                var result = await _handler.Handle(
                    query,
                    CancellationToken.None);

                // Assert
                result.TotalVehicles.Should().Be(3);
                result.AvailableVehicles.Should().Be(1);
                result.AssignedVehicles.Should().Be(1);
                result.VehiclesInMaintenance.Should().Be(1);
            }
        [Fact]
        public async Task Handle_Should_Return_Correct_Driver_Counts()
        {
            // Arrange
            var activeDriver1 = new Driver(
                "EMP001",
                "John",
                "Doe",
                "john@example.com",
                "0111111111",
                "LIC001",
                DateTime.UtcNow.AddYears(2));

            var activeDriver2 = new Driver(
                "EMP002",
                "Jane",
                "Doe",
                "jane@example.com",
                "0111111112",
                "LIC002",
                DateTime.UtcNow.AddYears(2));

            var inactiveDriver = new Driver(
                "EMP003",
                "Peter",
                "Smith",
                "peter@example.com",
                "0111111113",
                "LIC003",
                DateTime.UtcNow.AddYears(2));

            inactiveDriver.UpdateDetails(
                "EMP003",
                "Peter",
                "Smith",
                "peter@example.com",
                "0111111113",
                "LIC003",
                DateTime.UtcNow.AddYears(2),
                DriverStatus.Inactive);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>
                {
            activeDriver1,
            activeDriver2,
            inactiveDriver
                });

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalDrivers.Should().Be(3);
            result.ActiveDrivers.Should().Be(2);
        }
        [Fact]
        public async Task Handle_Should_Calculate_Financial_Costs_Correctly()
        {
            // Arrange
            var vehicle1 = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var vehicle2 = new Vehicle(
                "TEST002",
                "VIN002",
                "Ford",
                "Ranger",
                2026,
                FuelType.Diesel);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>
                {
            vehicle1,
            vehicle2
                });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            new FuelTransaction(
                vehicle1.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null),

            new FuelTransaction(
                vehicle2.Id,
                DateTime.UtcNow,
                2000,
                50,
                0,
                "Diesel",
                "Test Station",
                null,
                null)
                });

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>
                {
            new Maintenance(
                vehicle1.Id,
                "Major Service",
                "Engine service",
                DateTime.UtcNow,
                1000,
                4500,
                null)
                });

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should().Be(1500);
            result.TotalMaintenanceCost.Should().Be(4500);
            result.TotalOperatingCost.Should().Be(6000);

            result.AverageFuelCostPerVehicle.Should().Be(750);
            result.AverageMaintenanceCostPerVehicle.Should().Be(2250);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_From_Date_Is_Later_Than_To_Date()
        {
            // Arrange
            var from = new DateTime(2026, 10, 10);
            var to = new DateTime(2026, 10, 1);

            var query = new GetFleetDashboardQuery(from, to);

            // Act
            Func<Task> act = async () =>
                await _handler.Handle(
                    query,
                    CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("The 'From' date cannot be later than the 'To' date.");
        }

        [Fact]
        public async Task Handle_Should_Calculate_Trip_Distance_Correctly()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var trip1 = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Johannesburg",
                "Pretoria",
                DateTime.UtcNow,
                1000,
                null);

            var trip2 = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Pretoria",
                "Johannesburg",
                DateTime.UtcNow,
                1500,
                null);

            // Complete the trips
            trip1.UpdateDetails(
                "Johannesburg",
                "Pretoria",
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                1000,
                1200,
                TripStatus.Completed,
                null);

            trip2.UpdateDetails(
                "Pretoria",
                "Johannesburg",
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                1500,
                1800,
                TripStatus.Completed,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>
                {
            trip1,
            trip2
                });

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(2);
            result.TotalDistanceTravelled.Should().Be(500);
            result.AverageDistancePerTrip.Should().Be(250);
        }
        [Fact]
        public async Task Handle_Should_Ignore_Trips_Without_EndMileage()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var vehicleId = Guid.NewGuid();
            var driverId = Guid.NewGuid();

            var completedTrip = new Trip(
                vehicleId,
                driverId,
                "Johannesburg",
                "Pretoria",
                DateTime.UtcNow,
                1000,
                null);

            completedTrip.UpdateDetails(
                "Johannesburg",
                "Pretoria",
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                1000,
                1300,
                TripStatus.Completed,
                null);

            var incompleteTrip = new Trip(
                vehicleId,
                driverId,
                "Pretoria",
                "Johannesburg",
                DateTime.UtcNow,
                1300,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>
                {
            completedTrip,
            incompleteTrip
                });

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(2);
            result.TotalDistanceTravelled.Should().Be(300);
            result.AverageDistancePerTrip.Should().Be(300);
        }
        [Fact]
        public async Task Handle_Should_Filter_Fuel_Transactions_By_Date_Range()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);
            var toDate = new DateTime(2026, 10, 31);

            var fuelInsideRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            var fuelOutsideRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                2000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            // Set CreatedAt values for the test
            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(
                    fuelInsideRange,
                    new DateTime(2026, 10, 15));

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(
                    fuelOutsideRange,
                    new DateTime(2026, 11, 15));

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>
                {
            vehicle
                });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            fuelInsideRange,
            fuelOutsideRange
                });

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                fromDate,
                toDate);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should().Be(
                fuelInsideRange.TotalCost);
        }


        [Fact]
        public async Task Handle_Should_Filter_Maintenance_By_Date_Range()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);
            var toDate = new DateTime(2026, 10, 31);

            var maintenanceInsideRange = new Maintenance(
                vehicle.Id,
                "Major Service",
                "Engine service",
                new DateTime(2026, 10, 15),
                1000,
                4500,
                null);

            var maintenanceOutsideRange = new Maintenance(
                vehicle.Id,
                "Brake Service",
                "Brake replacement",
                new DateTime(2026, 11, 15),
                2000,
                3000,
                null);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>
                {
            vehicle
                });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>
                {
            maintenanceInsideRange,
            maintenanceOutsideRange
                });

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                fromDate,
                toDate);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalMaintenanceCost.Should().Be(
                maintenanceInsideRange.Cost);
        }
        [Fact]
        public async Task Handle_Should_Filter_Trips_By_Date_Range()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var trip1 = new Fleet.Core.Entities.Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Johannesburg",
                "Pretoria",
                DateTime.UtcNow,
                1000,
                null);

            var trip2 = new Fleet.Core.Entities.Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Pretoria",
                "Johannesburg",
                DateTime.UtcNow,
                1500,
                null);

            // Complete the trips
            trip1.UpdateDetails(
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 10, 15),
                new DateTime(2026, 10, 15).AddHours(2),
                1000,
                1200,
                TripStatus.Completed,
                null);

            trip2.UpdateDetails(
                "Pretoria",
                "Johannesburg",
                new DateTime(2026, 11, 15),
                new DateTime(2026, 11, 15).AddHours(2),
                1500,
                1800,
                TripStatus.Completed,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Fleet.Core.Entities.Trip>
                {
            trip1,
            trip2
                });

            var query = new GetFleetDashboardQuery(
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 31));

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(1);
            result.TotalDistanceTravelled.Should().Be(200);
            result.AverageDistancePerTrip.Should().Be(200);
        }

        [Fact]
        public async Task Handle_Should_Return_Zero_Distance_When_All_Trips_Are_Incomplete()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var trip1 = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 10, 5),
                1000,
                null);

            var trip2 = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Pretoria",
                "Durban",
                new DateTime(2026, 10, 6),
                1500,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>
                {
            trip1,
            trip2
                });

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(2);
            result.TotalDistanceTravelled.Should().Be(0);
            result.AverageDistancePerTrip.Should().Be(0);
        }

        [Fact]
        public async Task Handle_Should_Return_Zero_Trip_Metrics_When_No_Trips_Exist()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(null, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(0);
            result.TotalDistanceTravelled.Should().Be(0);
            result.AverageDistancePerTrip.Should().Be(0);
        }

        [Fact]
        public async Task Handle_Should_Include_Trips_On_To_Date()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var trip = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 10, 31, 23, 30, 0),
                1000,
                null);

            trip.UpdateDetails(
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 10, 31, 23, 30, 0),
                new DateTime(2026, 11, 1, 1, 30, 0),
                1000,
                1200,
                TripStatus.Completed,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip> { trip });

            var query = new GetFleetDashboardQuery(
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 31));

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(1);
            result.TotalDistanceTravelled.Should().Be(200);
        }

        [Fact]
        public async Task Handle_Should_Exclude_Trips_Before_From_Date()
        {
            // Arrange
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle>());

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            var tripBeforeRange = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 9, 30),
                1000,
                null);

            var tripInsideRange = new Trip(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Pretoria",
                "Durban",
                new DateTime(2026, 10, 15),
                1500,
                null);

            tripBeforeRange.UpdateDetails(
                "Johannesburg",
                "Pretoria",
                new DateTime(2026, 9, 30),
                new DateTime(2026, 9, 30).AddHours(2),
                1000,
                1200,
                TripStatus.Completed,
                null);

            tripInsideRange.UpdateDetails(
                "Pretoria",
                "Durban",
                new DateTime(2026, 10, 15),
                new DateTime(2026, 10, 15).AddHours(2),
                1500,
                1800,
                TripStatus.Completed,
                null);

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>
                {
            tripBeforeRange,
            tripInsideRange
                });

            var query = new GetFleetDashboardQuery(
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 31));

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalTrips.Should().Be(1);
            result.TotalDistanceTravelled.Should().Be(300);
            result.AverageDistancePerTrip.Should().Be(300);
        }

        [Fact]
        public async Task Handle_Should_Filter_Fuel_Transactions_By_From_Date_Only()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);

            var fuelBeforeRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            var fuelInsideRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                2000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            // Set CreatedAt explicitly for predictable test dates.
            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(fuelBeforeRange, new DateTime(2026, 9, 15));

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(fuelInsideRange, new DateTime(2026, 10, 15));

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            fuelBeforeRange,
            fuelInsideRange
                });

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(fromDate, null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should().Be(fuelInsideRange.TotalCost);
        }

        [Fact]
        public async Task Handle_Should_Filter_Fuel_Transactions_By_To_Date_Only()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var toDate = new DateTime(2026, 10, 31);

            var fuelInsideRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            var fuelAfterRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                2000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(fuelInsideRange, new DateTime(2026, 10, 15));

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(fuelAfterRange, new DateTime(2026, 11, 15));

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            fuelInsideRange,
            fuelAfterRange
                });

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(null, toDate);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should().Be(fuelInsideRange.TotalCost);
        }


        [Fact]
        public async Task Handle_Should_Exclude_Fuel_Transactions_Before_From_Date()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);

            var fuelBeforeRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            var fuelInsideRange = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                2000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            // Set transaction dates for testing
            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(
                    fuelBeforeRange,
                    new DateTime(2026, 9, 30));

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(
                    fuelInsideRange,
                    new DateTime(2026, 10, 15));

            // Mock vehicle repository
            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            // Mock driver repository
            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            // Mock fuel transaction repository
            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            fuelBeforeRange,
            fuelInsideRange
                });

            // Mock maintenance repository
            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            // Mock trip repository
            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                fromDate,
                null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should()
                .Be(fuelInsideRange.TotalCost);
        }

    
        [Fact]
        public async Task Handle_Should_Include_Fuel_Transactions_On_From_Date()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);

            var fuelOnFromDate = new FuelTransaction(
                vehicle.Id,
                DateTime.UtcNow,
                1000,
                50,
                30,
                "Diesel",
                "Test Station",
                null,
                null);

            typeof(FuelTransaction)
                .GetProperty(nameof(FuelTransaction.CreatedAt))!
                .SetValue(
                    fuelOnFromDate,
                    new DateTime(2026, 10, 1, 0, 0, 0));

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>
                {
            fuelOnFromDate
                });

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>());

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                fromDate,
                null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalFuelCost.Should()
                .Be(fuelOnFromDate.TotalCost);
        }

    
        [Fact]
        public async Task Handle_Should_Filter_Maintenance_By_From_Date_Only()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var fromDate = new DateTime(2026, 10, 1);

            var maintenanceBeforeRange = new Maintenance(
                vehicle.Id,
                "Brake Service",
                "Brake replacement",
                new DateTime(2026, 9, 15),
                1000,
                3000,
                null);

            var maintenanceInsideRange = new Maintenance(
                vehicle.Id,
                "Major Service",
                "Engine service",
                new DateTime(2026, 10, 15),
                2000,
                4500,
                null);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>
                {
            maintenanceBeforeRange,
            maintenanceInsideRange
                });

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                fromDate,
                null);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalMaintenanceCost.Should()
                .Be(maintenanceInsideRange.Cost);
        }
   
        [Fact]
        public async Task Handle_Should_Filter_Maintenance_By_To_Date_Only()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var toDate = new DateTime(2026, 10, 31);

            var maintenanceInsideRange = new Maintenance(
                vehicle.Id,
                "Major Service",
                "Engine service",
                new DateTime(2026, 10, 15),
                1000,
                4500,
                null);

            var maintenanceAfterRange = new Maintenance(
                vehicle.Id,
                "Brake Service",
                "Brake replacement",
                new DateTime(2026, 11, 15),
                2000,
                3000,
                null);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>
                {
            maintenanceInsideRange,
            maintenanceAfterRange
                });

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                null,
                toDate);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalMaintenanceCost.Should()
                .Be(maintenanceInsideRange.Cost);
        }
      
        [Fact]
        public async Task Handle_Should_Include_Maintenance_On_To_Date()
        {
            // Arrange
            var vehicle = new Vehicle(
                "TEST001",
                "VIN001",
                "Toyota",
                "Hilux",
                2025,
                FuelType.Diesel);

            var toDate = new DateTime(2026, 10, 31);

            var maintenanceOnToDate = new Maintenance(
                vehicle.Id,
                "Major Service",
                "Engine service",
                new DateTime(2026, 10, 31, 23, 30, 0),
                1000,
                4500,
                null);

            _vehicleRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Vehicle> { vehicle });

            _driverRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Driver>());

            _fuelRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FuelTransaction>());

            _maintenanceRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Maintenance>
                {
            maintenanceOnToDate
                });

            _tripRepository
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Trip>());

            var query = new GetFleetDashboardQuery(
                null,
                toDate);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.TotalMaintenanceCost.Should()
                .Be(maintenanceOnToDate.Cost);
        }

    }
    }

