

namespace Fleet.Application.Features.FuelTransactions.DTOs
{
    public record FuelTransactionResponse(
    Guid Id,
    Guid VehicleId,
    DateTime TransactionDate,
    int Mileage,
    decimal Litres,
    decimal PricePerLitre,
    decimal TotalCost,
    string FuelType,
    string? FuelStation,
    string? ReceiptNumber,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
}
