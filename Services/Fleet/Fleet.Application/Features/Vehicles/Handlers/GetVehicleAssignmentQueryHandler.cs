using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignment;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class GetVehicleAssignmentQueryHandler
    : IRequestHandler<GetVehicleAssignmentQuery, VehicleAssignmentDto?>
    {
        private readonly IVehicleAssignmentRepository _assignmentRepository;

        public GetVehicleAssignmentQueryHandler(
            IVehicleAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<VehicleAssignmentDto?> Handle(
            GetVehicleAssignmentQuery query,
            CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(
                query.Id,
                cancellationToken);

            if (assignment is null)
            {
                return null;
            }

            return new VehicleAssignmentDto(
                assignment.Id,
                assignment.VehicleId,
                assignment.DriverId,
                assignment.AssignedAt,
                assignment.UnassignedAt);
        }
    }
}
