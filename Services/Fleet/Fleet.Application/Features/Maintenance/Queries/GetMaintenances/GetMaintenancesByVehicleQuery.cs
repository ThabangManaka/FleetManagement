using MediatR;


namespace Fleet.Application.Features.Maintenance.Queries.GetMaintenances
{
    public record GetMaintenancesByVehicleQuery(
          Guid VehicleId
      ) : IRequest<List<MaintenanceResponse>>;
}
