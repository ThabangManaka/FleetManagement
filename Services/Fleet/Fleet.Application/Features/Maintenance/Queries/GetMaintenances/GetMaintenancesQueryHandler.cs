using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.Maintenance.Queries.GetMaintenances
{
    public class GetMaintenancesQueryHandler
        : IRequestHandler<GetMaintenancesQuery, List<MaintenanceResponse>>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public GetMaintenancesQueryHandler(
            IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<List<MaintenanceResponse>> Handle(
            GetMaintenancesQuery query,
            CancellationToken cancellationToken)
        {
            var maintenances = await _maintenanceRepository.GetAllAsync(
                cancellationToken);

            return maintenances
                .Select(maintenance => new MaintenanceResponse(
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
                    maintenance.UpdatedAt))
                .ToList();
        }
    }
}
