

namespace Fleet.Application.Features.FuelTransactions.DTOs
{
    public record VehicleSummaryResponse(
            Guid VehicleId,
            string RegistrationNumber,
            string Make,
            string Model,
            string Status,
            decimal CurrentMileage,
            int TotalFuelTransactions,
            decimal TotalFuelCost,
            decimal FuelEfficiency,
            decimal FuelCostPer100Km,
            int TotalMaintenanceRecords,
            decimal TotalMaintenanceCost,
            DateTime? LastMaintenanceDate
        );

}
