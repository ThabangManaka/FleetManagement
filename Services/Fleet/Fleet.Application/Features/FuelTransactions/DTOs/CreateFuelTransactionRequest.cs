

namespace Fleet.Application.Features.FuelTransactions.DTOs
{
    public record CreateFuelTransactionRequest(
         Guid VehicleId,
         DateTime TransactionDate,
         int Mileage,
         decimal Litres,
         decimal PricePerLitre,
         string FuelType,
         string? FuelStation,
         string? ReceiptNumber,
         string? Notes);
}
