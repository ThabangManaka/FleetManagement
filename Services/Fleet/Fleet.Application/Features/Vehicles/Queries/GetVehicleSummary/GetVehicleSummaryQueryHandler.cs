using Fleet.Application.Features.FuelTransactions.DTOs;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Queries.GetVehicleSummary
{
    public class GetVehicleSummaryQueryHandler
           : IRequestHandler<GetVehicleSummaryQuery, VehicleSummaryResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFuelTransactionRepository _fuelRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;

        public GetVehicleSummaryQueryHandler(
            IVehicleRepository vehicleRepository,
            IFuelTransactionRepository fuelRepository,
            IMaintenanceRepository maintenanceRepository)
        {
            _vehicleRepository = vehicleRepository;
            _fuelRepository = fuelRepository;
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<VehicleSummaryResponse> Handle(
            GetVehicleSummaryQuery query,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                query.VehicleId,
                cancellationToken);

            if (vehicle == null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle {query.VehicleId} was not found.");
            }

            var transactions = await _fuelRepository.GetByVehicleIdAsync(
                query.VehicleId,
                cancellationToken);

            var maintenances = await _maintenanceRepository.GetByVehicleIdAsync(
                query.VehicleId,
                cancellationToken);

            // Fuel summary
            var totalFuelTransactions = transactions.Count;

            var totalFuelCost = transactions.Sum(x => x.TotalCost);

            var fuelEfficiency = 0m;
            var fuelCostPer100Km = 0m;

            if (transactions.Count > 1)
            {
                var firstMileage = transactions.First().Mileage;
                var lastMileage = transactions.Last().Mileage;

                var distanceTravelled =
                    lastMileage - firstMileage;

                var fuelConsumed = transactions
                    .Skip(1)
                    .Sum(x => x.Litres);

                if (fuelConsumed > 0)
                {
                    fuelEfficiency =
                        distanceTravelled / fuelConsumed;
                }

                if (distanceTravelled > 0)
                {
                    var fuelCost = transactions
                        .Skip(1)
                        .Sum(x => x.TotalCost);

                    fuelCostPer100Km =
                        (fuelCost / distanceTravelled) * 100;
                }
            }

            // Maintenance summary
            var totalMaintenanceRecords = maintenances.Count;

            var totalMaintenanceCost = maintenances.Sum(
                x => x.Cost);

            DateTime? lastMaintenanceDate = maintenances
                .OrderByDescending(x => x.ServiceDate)
                .Select(x => (DateTime?)x.ServiceDate)
                .FirstOrDefault();

            return new VehicleSummaryResponse(
                vehicle.Id,
                vehicle.RegistrationNumber,
                vehicle.Make,
                vehicle.Model,
                vehicle.Status.ToString(),
                vehicle.Mileage,
                totalFuelTransactions,
                totalFuelCost,
                fuelEfficiency,
                fuelCostPer100Km,
                totalMaintenanceRecords,
                totalMaintenanceCost,
                lastMaintenanceDate);
        }
    }
}