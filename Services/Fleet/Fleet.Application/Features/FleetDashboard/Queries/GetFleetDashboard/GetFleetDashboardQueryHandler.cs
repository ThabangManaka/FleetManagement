using Fleet.Application.Features.FleetDashboard.DTOs;
using Fleet.Application.Interfaces;
using Fleet.Core.Enums;
using MediatR;


namespace Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard
{
    public class GetFleetDashboardQueryHandler
        : IRequestHandler<GetFleetDashboardQuery, FleetDashboardResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IFuelTransactionRepository _fuelRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly ITripRepository _tripRepository;

        public GetFleetDashboardQueryHandler(
            IVehicleRepository vehicleRepository,
            IDriverRepository driverRepository,
            IFuelTransactionRepository fuelRepository,
            IMaintenanceRepository maintenanceRepository,
            ITripRepository tripRepository)
        {
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
            _fuelRepository = fuelRepository;
            _maintenanceRepository = maintenanceRepository;
            _tripRepository = tripRepository;
        }

        public async Task<FleetDashboardResponse> Handle(
    GetFleetDashboardQuery query,
    CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(
                cancellationToken);

            var drivers = await _driverRepository.GetAllAsync(
                cancellationToken);

            var fuelTransactions = await _fuelRepository.GetAllAsync(
                cancellationToken);

            var maintenances = await _maintenanceRepository.GetAllAsync(
                cancellationToken);

            var trips = await _tripRepository.GetAllAsync(
                cancellationToken);

            var totalVehicles = vehicles.Count;

            var availableVehicles = vehicles.Count(x =>
                x.Status == VehicleStatus.Available);

            var assignedVehicles = vehicles.Count(x =>
                x.Status == VehicleStatus.Assigned);

            var vehiclesInMaintenance = vehicles.Count(x =>
                x.Status == VehicleStatus.Maintenance);

            var totalDrivers = drivers.Count;

            var activeDrivers = drivers.Count(x =>
                x.Status == DriverStatus.Active);

            var totalFuelCost = fuelTransactions.Sum(x =>
                x.TotalCost);

            var totalMaintenanceCost = maintenances.Sum(x =>
                x.Cost);

            var totalTrips = trips.Count;

            var totalDistanceTravelled = trips
                 .Where(x => x.EndMileage.HasValue)
                 .Sum(x => x.EndMileage!.Value - x.StartMileage);

            return new FleetDashboardResponse(
                totalVehicles,
                availableVehicles,
                assignedVehicles,
                vehiclesInMaintenance,
                totalDrivers,
                activeDrivers,
                totalFuelCost,
                totalMaintenanceCost,
                totalTrips,
                totalDistanceTravelled);
        }
    }
}