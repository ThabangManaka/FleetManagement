using Fleet.Application.Features.Vehicles.Commands.AssignDriver;
using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class AssignDriverCommandHandler
      : IRequestHandler<AssignDriverCommand, Guid>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleAssignmentRepository _assignmentRepository;

        public AssignDriverCommandHandler(
            IVehicleRepository vehicleRepository,
            IDriverRepository driverRepository,
            IVehicleAssignmentRepository assignmentRepository)
        {
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<Guid> Handle(
            AssignDriverCommand command,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                command.VehicleId,
                cancellationToken);

            if (vehicle is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle '{command.VehicleId}' was not found.");
            }

            var driver = await _driverRepository.GetByIdAsync(
                command.DriverId,
                cancellationToken);

            if (driver is null)
            {
                throw new KeyNotFoundException(
                    $"Driver '{command.DriverId}' was not found.");
            }

            if (await _assignmentRepository.HasActiveAssignmentForVehicleAsync(
                command.VehicleId,
                cancellationToken))
            {
                throw new InvalidOperationException(
                    "The vehicle is already assigned to a driver.");
            }

            if (await _assignmentRepository.HasActiveAssignmentForDriverAsync(
                command.DriverId,
                cancellationToken))
            {
                throw new InvalidOperationException(
                    "The driver is already assigned to a vehicle.");
            }

            var assignment = new VehicleAssignment(
                command.VehicleId,
                command.DriverId);

            await _assignmentRepository.AddAsync(
                assignment,
                cancellationToken);

            await _assignmentRepository.SaveChangesAsync(
                cancellationToken);

            return assignment.Id;
        }
    }
}
