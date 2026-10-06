using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard;
using Fleet.Application.Features.FleetDashboard.DTOs;
using Fleet.Application.Interfaces;

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
    }
}
