
using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Commands
{
    public record UpdateVehicleCommand(
        Guid Id,
        UpdateVehicleRequest Request
    ) : IRequest<VehicleResponse>;
}
