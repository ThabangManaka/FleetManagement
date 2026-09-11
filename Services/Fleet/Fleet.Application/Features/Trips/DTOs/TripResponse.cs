using Fleet.Core.Enums;

namespace Fleet.Application.Features.Trips.DTOs
{
    public record TripResponse(
    Guid Id,
    Guid VehicleId,
    Guid DriverId,
    string StartLocation,
    string Destination,
    DateTime StartDate,
    DateTime? EndDate,
    int StartMileage,
    int? EndMileage,
    TripStatus Status,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
}
