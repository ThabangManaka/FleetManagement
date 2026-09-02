using Fleet.Application.Features.Vehicles.Commands.UnassignDriver;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class UnassignDriverCommandHandler
    : IRequestHandler<UnassignDriverCommand>
    {
        private readonly IVehicleAssignmentRepository _assignmentRepository;

        public UnassignDriverCommandHandler(
            IVehicleAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task Handle(
            UnassignDriverCommand command,
            CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (assignment is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle assignment '{command.Id}' was not found.");
            }

            assignment.Unassign();

            await _assignmentRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}
