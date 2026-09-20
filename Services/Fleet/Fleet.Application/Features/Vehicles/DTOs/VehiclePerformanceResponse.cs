
namespace Fleet.Application.Features.Vehicles.DTOs
{
    public record VehiclePerformanceResponse(
    Guid VehicleId,
    string RegistrationNumber,
    string Make,
    string Model,
    decimal CurrentMileage,
    decimal FuelEfficiency,
    decimal FuelCostPer100Km,
    decimal TotalFuelCost,
    decimal TotalMaintenanceCost,
    decimal TotalOperatingCost,
    int TotalTrips,
    decimal TotalDistanceTravelled
);
}
