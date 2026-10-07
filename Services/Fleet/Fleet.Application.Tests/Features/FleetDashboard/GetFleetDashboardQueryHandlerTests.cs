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
    }
    }

