

namespace Fleet.Application.Features.FuelTransactions.DTOs
{
    public record UpdateFuelTransactionRequest(
      DateTime TransactionDate,
      int Mileage,
      decimal Litres,
      decimal PricePerLitre,
      string FuelType,
      string? FuelStation,
      string? ReceiptNumber,
      string? Notes);
}
