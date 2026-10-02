using Fleet.Application.Features.FuelTransactions.DTOs;
using Fleet.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.FuelTransactions.Queries
{
    public class GetFuelTransactionByIdQueryHandler
           : IRequestHandler<
               GetFuelTransactionByIdQuery,
               FuelTransactionResponse>
    {
        private readonly IFuelTransactionRepository _fuelRepository;

        public GetFuelTransactionByIdQueryHandler(
            IFuelTransactionRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task<FuelTransactionResponse> Handle(
            GetFuelTransactionByIdQuery query,
            CancellationToken cancellationToken)
        {
            var fuelTransaction =
                await _fuelRepository.GetByIdAsync(
                    query.Id,
                    cancellationToken);

            if (fuelTransaction == null)
            {
                throw new KeyNotFoundException(
                    $"Fuel transaction '{query.Id}' was not found.");
            }

            return new FuelTransactionResponse(
                fuelTransaction.Id,
                fuelTransaction.VehicleId,
                fuelTransaction.TransactionDate,
                fuelTransaction.Mileage,
                fuelTransaction.Litres,
                fuelTransaction.PricePerLitre,
                fuelTransaction.TotalCost,
                fuelTransaction.FuelType,
                fuelTransaction.FuelStation,
                fuelTransaction.ReceiptNumber,
                fuelTransaction.Notes,
                fuelTransaction.CreatedAt,
                fuelTransaction.UpdatedAt);
        }
    }
}
