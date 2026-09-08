using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class DeleteMaintenanceCommandHandler
        : IRequestHandler<DeleteMaintenanceCommand>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public DeleteMaintenanceCommandHandler(
            IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task Handle(
            DeleteMaintenanceCommand command,
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

            _maintenanceRepository.Delete(maintenance);

            await _maintenanceRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}