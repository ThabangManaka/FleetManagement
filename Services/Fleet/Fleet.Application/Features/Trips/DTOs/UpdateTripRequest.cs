using Fleet.Core.Enums;

namespace Fleet.Application.Features.Trips.DTOs
{
    public record UpdateTripRequest(
       string StartLocation,
       string Destination,
       DateTime StartDate,
       DateTime? EndDate,
       int StartMileage,
       int? EndMileage,
       TripStatus Status,
       string? Notes);
}
