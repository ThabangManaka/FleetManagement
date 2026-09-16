

namespace Fleet.Application.Features.FuelTransactions.DTOs
{
    public record FuelConsumptionResponse(
   Guid VehicleId,
        int TotalFuelTransactions,
        decimal TotalLitres,
        decimal TotalFuelCost,
        decimal AveragePricePerLitre,
        int DistanceTravelled,
        decimal FuelEfficiency,
        decimal CostPerKilometre);

}
