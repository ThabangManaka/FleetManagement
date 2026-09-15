using Fleet.Application.Features.FuelTransactions.DTOs;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.FuelTransactions.Queries
{
    public class GetFuelTransactionsQueryHandler
            : IRequestHandler<GetFuelTransactionsQuery, List<FuelTransactionResponse>>
    {
        private readonly IFuelTransactionRepository _fuelRepository;

        public GetFuelTransactionsQueryHandler(
            IFuelTransactionRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task<List<FuelTransactionResponse>> Handle(
            GetFuelTransactionsQuery query,
            CancellationToken cancellationToken)
        {
            var fuelTransactions = await _fuelRepository.GetAllAsync(
                cancellationToken);

            return fuelTransactions
                .Select(x => new FuelTransactionResponse(
                    x.Id,
                    x.VehicleId,
                    x.TransactionDate,
                    x.Mileage,
                    x.Litres,
                    x.PricePerLitre,
                    x.TotalCost,
                    x.FuelType,
                    x.FuelStation,
                    x.ReceiptNumber,
                    x.Notes,
                    x.CreatedAt,
                    x.UpdatedAt))
                .ToList();
        }
    }
}
