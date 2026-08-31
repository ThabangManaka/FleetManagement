using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignments;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
     public class GetVehicleAssignmentsQueryHandler
        : IRequestHandler<GetVehicleAssignmentsQuery, List<VehicleAssignmentDto>>
    {
        private readonly IVehicleAssignmentRepository _assignmentRepository;

        public GetVehicleAssignmentsQueryHandler(
            IVehicleAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<VehicleAssignmentDto>> Handle(
            GetVehicleAssignmentsQuery query,
            CancellationToken cancellationToken)
        {
            var assignments = await _assignmentRepository.GetAllAsync(
                cancellationToken);

            return assignments
                .Select(assignment => new VehicleAssignmentDto(
                    assignment.Id,
                    assignment.VehicleId,
                    assignment.DriverId,
                    assignment.AssignedAt,
                    assignment.UnassignedAt))
                .ToList();
        }
    }
}
