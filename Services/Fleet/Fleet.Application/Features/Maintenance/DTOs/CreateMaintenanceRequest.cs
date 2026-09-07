namespace Fleet.Application.Features.Maintenance.DTOs
{
    public record CreateMaintenanceRequest(
      Guid VehicleId,
      string MaintenanceType,
      string Description,
      DateTime ServiceDate,
      int Mileage,
      decimal Cost,
      string? Notes);
}
