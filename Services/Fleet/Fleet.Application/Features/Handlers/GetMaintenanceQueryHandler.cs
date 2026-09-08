using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class GetMaintenanceQueryHandler
        : IRequestHandler<GetMaintenanceQuery, MaintenanceResponse?>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public GetMaintenanceQueryHandler(
            IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<MaintenanceResponse?> Handle(
            GetMaintenanceQuery query,
            CancellationToken cancellationToken)
        {
            var maintenance = await _maintenanceRepository.GetByIdAsync(
                query.Id,
                cancellationToken);

            if (maintenance is null)
            {
                return null;
            }

            return new MaintenanceResponse(
                maintenance.Id,
                maintenance.VehicleId,
                maintenance.MaintenanceType,
                maintenance.Description,
                maintenance.ServiceDate,
                maintenance.Mileage,
                maintenance.Cost,
                maintenance.Notes,
                maintenance.Status,
                maintenance.CreatedAt,
                maintenance.UpdatedAt);
        }
    }
}