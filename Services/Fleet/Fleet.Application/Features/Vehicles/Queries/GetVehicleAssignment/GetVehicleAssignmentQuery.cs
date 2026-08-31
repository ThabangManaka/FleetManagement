using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignment
{
    public record GetVehicleAssignmentQuery(
        Guid Id
    ) : IRequest<VehicleAssignmentDto?>;
}
