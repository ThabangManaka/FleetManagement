using Fleet.Application.Features.FleetDashboard.Queries.GetFleetDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FleetDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetFleetDashboardQuery(),
                cancellationToken);

            return Ok(result);
        }
    }
}