using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record AssignDriverCommand(
        Guid VehicleId,
        Guid DriverId
    ) : IRequest<Guid>;
}