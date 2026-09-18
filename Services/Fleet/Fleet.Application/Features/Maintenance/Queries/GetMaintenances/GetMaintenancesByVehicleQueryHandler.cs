using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.Maintenance.Queries.GetMaintenances
{
    public class GetMaintenancesByVehicleQueryHandler
     : IRequestHandler<
         GetMaintenancesByVehicleQuery,
         List<MaintenanceResponse>>
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public GetMaintenancesByVehicleQueryHandler(
            IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public async Task<List<MaintenanceResponse>> Handle(
            GetMaintenancesByVehicleQuery query,
            CancellationToken cancellationToken)
        {
            var maintenances =
                await _maintenanceRepository.GetByVehicleIdAsync(
                    query.VehicleId,
                    cancellationToken);

            return maintenances
                .Select(x => new MaintenanceResponse(
                    x.Id,
                    x.VehicleId,
                    x.MaintenanceType,
                    x.Description,
                    x.ServiceDate,
                    x.Mileage,
                    x.Cost,
                    x.Notes,
                    x.Status,
                    x.CreatedAt,
                    x.UpdatedAt))
                .ToList();
        }
    }
}
