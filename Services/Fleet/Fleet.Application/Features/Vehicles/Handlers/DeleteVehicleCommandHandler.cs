using Fleet.Application.Features.Vehicles.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteVehicleCommandHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(
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


            return Unit.Value;
        }
    }
}
