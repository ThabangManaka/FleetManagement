using Fleet.Application.Features.FleetDashboard.DTOs;
using MediatR;


namespace Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard
{
    public record GetFleetDashboardQuery
        : IRequest<FleetDashboardResponse>;
}