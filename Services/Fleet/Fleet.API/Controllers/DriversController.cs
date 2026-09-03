using Fleet.Application.Features.Commands;
using Fleet.Application.Features.Vehicles.Queries.GetDriver;
using Fleet.Application.Features.Vehicles.Queries.GetDrivers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDriverCommand command,
            CancellationToken cancellationToken)
        {
            var driverId = await _mediator.Send(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = driverId },
                driverId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetDriversQuery(),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetDriverQuery(id),
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
            [FromBody] UpdateDriverCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(
                    "The driver ID does not match.");
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
                new DeleteDriverCommand(id),
                cancellationToken);

            return NoContent();
        }
    }
}