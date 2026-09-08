using Fleet.Application.Features.Commands;
using Fleet.Application.Features.Maintenance.Queries.GetMaintenances;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly IMediator _mediator;

        public MaintenancesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateMaintenanceCommand command,
            CancellationToken cancellationToken)
        {
            var maintenanceId = await _mediator.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = maintenanceId },
                maintenanceId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetMaintenancesQuery(),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetMaintenanceQuery(id),
                cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateMaintenanceCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(
                    "The maintenance ID does not match.");
            }

            await _mediator.Send(
                command,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteMaintenanceCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}