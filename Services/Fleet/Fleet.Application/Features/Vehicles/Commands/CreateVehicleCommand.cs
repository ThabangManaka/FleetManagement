using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Commands
{
    public record CreateVehicleCommand(
        CreateVehicleRequest Request
    ) : IRequest<VehicleResponse>;
}