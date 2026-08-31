
namespace Fleet.Application.Features.Vehicles.DTOs
{
    public record VehicleAssignmentListDto(
    Guid Id,
    Guid VehicleId,
    Guid DriverId,
    DateTime AssignedAt,
    DateTime? UnassignedAt
);
}
