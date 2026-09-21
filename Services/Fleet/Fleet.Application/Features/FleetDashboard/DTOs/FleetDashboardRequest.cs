namespace Fleet.Application.Features.FleetDashboard.DTOs
{
    public record FleetDashboardRequest(
       DateTime? From,
       DateTime? To
   );
}
