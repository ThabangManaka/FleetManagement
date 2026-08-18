
using Fleet.Application.Features.Vehicles.DTOs;

namespace Fleet.Application.Features.Vehicles.Commands
{
    public record UpdateVehicleCommand(
    Guid Id, UpdateVehicleRequest Request);
}
