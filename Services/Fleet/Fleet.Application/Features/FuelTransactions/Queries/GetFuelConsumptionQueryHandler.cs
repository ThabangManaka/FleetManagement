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
                    0);
            }

            var totalLitres = transactions.Sum(x => x.Litres);

            var totalFuelCost = transactions.Sum(x => x.TotalCost);

            var averagePricePerLitre = transactions.Average(
                x => x.PricePerLitre);

            var firstMileage = transactions.First().Mileage;
            var lastMileage = transactions.Last().Mileage;

            var distanceTravelled = lastMileage - firstMileage;

            var fuelEfficiency = totalLitres > 0
                ? distanceTravelled / totalLitres
                : 0;

            return new FuelConsumptionResponse(
                query.VehicleId,
                transactions.Count,
                totalLitres,
                totalFuelCost,
                averagePricePerLitre,
                distanceTravelled,
                fuelEfficiency);
        }
    }
}