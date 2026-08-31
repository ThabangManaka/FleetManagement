using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignments;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class GetVehicleAssignmentsQueryHandler
    : IRequestHandler<GetVehicleAssignmentsQuery, List<VehicleAssignmentListDto>>
    {
        private readonly IVehicleAssignmentRepository _assignmentRepository;

        public GetVehicleAssignmentsQueryHandler(
            IVehicleAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<VehicleAssignmentListDto>> Handle(
            GetVehicleAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var assignments = await _assignmentRepository.GetAllAsync(
                cancellationToken);

            return assignments
                .Select(x => new VehicleAssignmentListDto(
                    x.Id,
                    x.VehicleId,
                    x.DriverId,
                    x.AssignedAt,
                    x.UnassignedAt))
                .ToList();
        }
    }
}
