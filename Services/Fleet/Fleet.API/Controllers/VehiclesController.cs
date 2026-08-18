using Fleet.Application.Features.Vehicles.Commands;
using Fleet.Application.Features.Vehicles.Handlers;
using Fleet.Application.Features.Vehicles.Queries.GetVehicle;
using Fleet.Application.Features.Vehicles.Queries.GetVehicles;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly CreateVehicleCommandHandler _createHandler;
        private readonly UpdateVehicleCommandHandler _updateHandler;
        private readonly DeleteVehicleCommandHandler _deleteHandler;
        private readonly GetVehicleQueryHandler _getHandler;
        private readonly GetVehiclesQueryHandler _getAllHandler;

        public VehiclesController(CreateVehicleCommandHandler createHandler,
            UpdateVehicleCommandHandler updateHandler,
            DeleteVehicleCommandHandler deleteHandler,
            GetVehicleQueryHandler getHandler,
            GetVehiclesQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getHandler = getHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        CreateVehicleCommand command,
        CancellationToken cancellationToken)
        {
            var result = await _createHandler.HandleAsync(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
     CancellationToken cancellationToken)
        {
            var result = await _getAllHandler.HandleAsync(
                new GetVehiclesQuery(),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
     Guid id,
     CancellationToken cancellationToken)
        {
            var result = await _getHandler.HandleAsync(
                new GetVehicleQuery(id),
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
        UpdateVehicleCommand command,
        CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("The vehicle ID does not match.");
            }

            var result = await _updateHandler.HandleAsync(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
