using Fleet.Application.Features.FleetDashboard.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard
{
    public record GetFleetDashboardQuery
        : IRequest<FleetDashboardResponse>;
}
