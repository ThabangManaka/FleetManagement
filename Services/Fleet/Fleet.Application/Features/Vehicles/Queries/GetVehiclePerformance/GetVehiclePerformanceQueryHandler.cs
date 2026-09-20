using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehiclePerformance
{
    public class GetVehiclePerformanceQueryHandler
        : IRequestHandler<
            GetVehiclePerformanceQuery,
            VehiclePerformanceResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFuelTransactionRepository _fuelRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly ITripRepository _tripRepository;

        public GetVehiclePerformanceQueryHandler(
            IVehicleRepository vehicleRepository,
            IFuelTransactionRepository fuelRepository,
            IMaintenanceRepository maintenanceRepository,
            ITripRepository tripRepository)
        {
            _vehicleRepository = vehicleRepository;
            _fuelRepository = fuelRepository;
            _maintenanceRepository = maintenanceRepository;
            _tripRepository = tripRepository;
        }

        public async Task<VehiclePerformanceResponse> Handle(
            GetVehiclePerformanceQuery query,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                query.VehicleId,
                cancellationToken);

            if (vehicle is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle with ID '{query.VehicleId}' was not found.");
            }

            var fuelTransactions =
                await _fuelRepository.GetByVehicleIdAsync(
                    query.VehicleId,
                    cancellationToken);

            var maintenances =
                await _maintenanceRepository.GetByVehicleIdAsync(
                    query.VehicleId,
                    cancellationToken);

            var trips = (await _tripRepository.GetAllAsync(
                cancellationToken))
                .Where(x => x.VehicleId == query.VehicleId)
                .ToList();

            decimal fuelEfficiency = 0;
            decimal fuelCostPer100Km = 0;

            if (fuelTransactions.Count >= 2)
            {
                var orderedTransactions = fuelTransactions
                    .OrderBy(x => x.Mileage)
                    .ToList();

                var startingMileage =
                    orderedTransactions.First().Mileage;

                var endingMileage =
                    orderedTransactions.Last().Mileage;

                var distanceTravelled =
                    endingMileage - startingMileage;

                var fuelConsumed = orderedTransactions
                    .Skip(1)
                    .Sum(x => x.Litres);

                var fuelCost = orderedTransactions
                    .Skip(1)
                    .Sum(x => x.TotalCost);

                if (fuelConsumed > 0 && distanceTravelled > 0)
                {
                    fuelEfficiency =
                        distanceTravelled / fuelConsumed;

                    fuelCostPer100Km =
                        (fuelCost / distanceTravelled) * 100;
                }
            }

            var totalFuelCost = fuelTransactions.Sum(
                x => x.TotalCost);

            var totalMaintenanceCost = maintenances.Sum(
                x => x.Cost);

            var totalOperatingCost =
                totalFuelCost + totalMaintenanceCost;

            var totalTrips = trips.Count;

            var totalDistanceTravelled = trips
                .Where(x => x.EndMileage.HasValue)
                .Sum(x =>
                    x.EndMileage!.Value - x.StartMileage);

            return new VehiclePerformanceResponse(
                vehicle.Id,
                vehicle.RegistrationNumber,
                vehicle.Make,
                vehicle.Model,
                vehicle.Mileage,
                fuelEfficiency,
                fuelCostPer100Km,
                totalFuelCost,
                totalMaintenanceCost,
                totalOperatingCost,
                totalTrips,
                totalDistanceTravelled);
        }
    }
}
