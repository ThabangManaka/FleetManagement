using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.FleetDashboard.DTOs
{
    public record FleetDashboardResponse(
        int TotalVehicles,
        int AvailableVehicles,
        int AssignedVehicles,
        int VehiclesInMaintenance,
        int TotalDrivers,
        int ActiveDrivers,
        decimal TotalFuelCost,
        decimal TotalMaintenanceCost,
        decimal TotalOperatingCost,
        decimal AverageFuelCostPerVehicle,
        decimal AverageMaintenanceCostPerVehicle,
        int TotalTrips,
        decimal TotalDistanceTravelled,
        decimal AverageDistancePerTrip
    );
}
