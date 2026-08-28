using Fleet.Application.Features.Commands;
using MediatR;

namespace Fleet.Application.Features.Handlers
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
                command.VehicleAssignmentId,
                cancellationToken);

            if (assignment is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle assignment with ID '{command.VehicleAssignmentId}' was not found.");
            }

            if (!assignment.IsActive())
            {
                throw new InvalidOperationException(
                    "This vehicle assignment has already been unassigned.");
            }

            assignment.Unassign();

            await _assignmentRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}