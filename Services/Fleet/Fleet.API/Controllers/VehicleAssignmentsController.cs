using Fleet.Application.Features.Commands;
using Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignment;
using Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleAssignmentsController : Controller
    {
        private readonly IMediator _mediator;

        public VehicleAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/VehicleAssignments
        [HttpPost]
        public async Task<ActionResult<Guid>> AssignDriver(
            [FromBody] AssignDriverCommand command)
        {
            var assignmentId = await _mediator.Send(command);

            return Ok(assignmentId);
        }

        // PUT: api/VehicleAssignments/{id}/unassign
        [HttpPut("{id:guid}/unassign")]
        public async Task<IActionResult> UnassignDriver(Guid id)
        {
            await _mediator.Send(
                new UnassignDriverCommand(id));

            return NoContent();
        }

        // GET: api/VehicleAssignments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _mediator.Send(
                new GetVehicleAssignmentsQuery());

            return Ok(assignments);
        }

        // GET: api/VehicleAssignments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var assignment = await _mediator.Send(
                new GetVehicleAssignmentQuery(id));

            if (assignment is null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }
    }
}
