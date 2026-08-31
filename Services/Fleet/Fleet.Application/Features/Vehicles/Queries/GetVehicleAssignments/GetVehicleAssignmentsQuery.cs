using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignments
{
    public record GetVehicleAssignmentsQuery
       : IRequest<List<VehicleAssignmentDto>>;
}
