using Fleet.Application.Features.Commands;
using Fleet.Application.Features.FuelTransactions.DTOs;
using Fleet.Application.Features.FuelTransactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuelTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FuelTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateFuelTransactionRequest request)
        {
            var command = new CreateFuelTransactionCommand(request);

            var id = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetFuelTransactionsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetFuelTransactionByIdQuery(id);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("vehicle/{vehicleId:guid}/consumption")]
        public async Task<IActionResult> GetFuelConsumption(Guid vehicleId)
        {
            var query = new GetFuelConsumptionQuery(vehicleId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateFuelTransactionRequest request)
        {
            var command = new UpdateFuelTransactionCommand(
                id,
                request);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFuelTransactionCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
