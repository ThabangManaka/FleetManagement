using MediatR;


namespace Fleet.Application.Features.Maintenance.Queries.GetMaintenances
{
    public record GetMaintenancesQuery
       : IRequest<List<MaintenanceResponse>>;
}
