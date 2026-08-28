using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record UnassignDriverCommand(
    Guid VehicleAssignmentId
) : IRequest;
}