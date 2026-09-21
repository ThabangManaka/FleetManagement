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
       [FromQuery] DateTime? from,
       [FromQuery] DateTime? to,
       CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetFleetDashboardQuery(from, to),
                cancellationToken);

            return Ok(result);
        }
    }
}