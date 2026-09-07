using Fleet.Core.Enums;

public record UpdateMaintenanceRequest(
        string MaintenanceType,
        string Description,
        DateTime ServiceDate,
        int Mileage,
        decimal Cost,
        string? Notes,
        MaintenanceStatus Status);
}
