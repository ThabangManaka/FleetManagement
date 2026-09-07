using Fleet.Core.Enums;

public record MaintenanceResponse(
     Guid Id,
     Guid VehicleId,
     string MaintenanceType,
     string Description,
     DateTime ServiceDate,
     int Mileage,
     decimal Cost,
     string? Notes,
     MaintenanceStatus Status,
     DateTime CreatedAt,
     DateTime? UpdatedAt);
