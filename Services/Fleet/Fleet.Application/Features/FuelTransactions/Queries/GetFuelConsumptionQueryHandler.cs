using Fleet.Application.Features.FuelTransactions.DTOs;
using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.FuelTransactions.Queries
{
    public class GetFuelConsumptionQueryHandler
        : IRequestHandler<GetFuelConsumptionQuery, FuelConsumptionResponse>
    {
        private readonly IFuelTransactionRepository _fuelRepository;

        public GetFuelConsumptionQueryHandler(
            IFuelTransactionRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task<FuelConsumptionResponse> Handle(
            GetFuelConsumptionQuery query,
            CancellationToken cancellationToken)
        {
            var transactions = await _fuelRepository.GetByVehicleIdAsync(
                query.VehicleId,
                cancellationToken);

            if (transactions.Count == 0)
            {
                return new FuelConsumptionResponse(
                    query.VehicleId,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0);
            }

            var totalLitres = transactions.Sum(x => x.Litres);

            var totalFuelCost = transactions.Sum(x => x.TotalCost);

            var averagePricePerLitre = transactions.Average(
                x => x.PricePerLitre);

            var firstMileage = transactions.First().Mileage;
            var lastMileage = transactions.Last().Mileage;

            var distanceTravelled = lastMileage - firstMileage;

            var fuelConsumed = transactions
            .Skip(1)
            .Sum(x => x.Litres);

            var fuelEfficiency = fuelConsumed > 0
                ? distanceTravelled / fuelConsumed
                : 0;

            var costPerKilometre = distanceTravelled > 0
            ? totalFuelCost / distanceTravelled
            : 0;

            var fuelCostPer100Km = costPerKilometre * 100;

            return new FuelConsumptionResponse(
                query.VehicleId,
                transactions.Count,
                totalLitres,
                totalFuelCost,
                averagePricePerLitre,
                distanceTravelled,
                fuelEfficiency,
                 costPerKilometre,
                 fuelCostPer100Km);
        }
    }
}