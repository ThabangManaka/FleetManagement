using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class UpdateMaintenanceCommandHandler
       : IRequestHandler<UpdateMaintenanceCommand>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public UpdateMaintenanceCommandHandler(
            IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task Handle(
            UpdateMaintenanceCommand command,
            CancellationToken cancellationToken)
        {
            var maintenance = await _maintenanceRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (maintenance is null)
            {
                throw new KeyNotFoundException(
                    $"Maintenance with ID '{command.Id}' was not found.");
            }

            maintenance.UpdateDetails(
                command.Request.MaintenanceType,
                command.Request.Description,
                command.Request.ServiceDate,
                command.Request.Mileage,
                command.Request.Cost,
                command.Request.Notes,
                command.Request.Status);

            _maintenanceRepository.Update(maintenance);

            await _maintenanceRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}