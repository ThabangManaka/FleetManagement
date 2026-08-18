using Fleet.Application.Features.Vehicles.Commands;
using Fleet.Application.Interfaces;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class DeleteVehicleCommandHandler
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteVehicleCommandHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task HandleAsync(
            DeleteVehicleCommand command,
            CancellationToken cancellationToken = default)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (vehicle == null)
            {
                throw new KeyNotFoundException(
                    "Vehicle not found.");
            }

            await _vehicleRepository.DeleteAsync(
                vehicle,
                cancellationToken);
        }
    }
}
