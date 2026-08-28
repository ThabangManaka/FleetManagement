
namespace Fleet.Application.Features.Vehicles.DTOs
{
    public record VehicleAssignmentDto(
    Guid Id,
    Guid VehicleId,
    Guid DriverId,
    DateTime AssignedAt,
    DateTime? UnassignedAt
);
}
