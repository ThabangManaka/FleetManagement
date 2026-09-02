using MediatR;

namespace Fleet.Application.Features.Vehicles.Commands.AssignDriver
{
    public record AssignDriverCommand(
       Guid VehicleId,
       Guid DriverId
   ) : IRequest<Guid>;
}

