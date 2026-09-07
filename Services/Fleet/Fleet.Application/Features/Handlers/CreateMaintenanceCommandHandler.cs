using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;

using MediatR;


namespace Fleet.Application.Features.Handlers
{
    public class CreateMaintenanceCommandHandler
           : IRequestHandler<CreateMaintenanceCommand, Guid>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CreateMaintenanceCommandHandler(
            IMaintenanceRepository maintenanceRepository,
            IVehicleRepository vehicleRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Guid> Handle(
            CreateMaintenanceCommand command,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                command.Request.VehicleId,
                cancellationToken);

            if (vehicle is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle with ID '{command.Request.VehicleId}' was not found.");
            }

            var maintenance = new global::Fleet.Core.Entities.Maintenance(
                command.Request.VehicleId,
                command.Request.MaintenanceType,
                command.Request.Description,
                command.Request.ServiceDate,
                command.Request.Mileage,
                command.Request.Cost,
                command.Request.Notes);

            await _maintenanceRepository.AddAsync(
                maintenance,
                cancellationToken);

            await _maintenanceRepository.SaveChangesAsync(
                cancellationToken);

            return maintenance.Id;
        }
    }
}
