using Fleet.Core.Entities;

namespace Fleet.Application
{
    public interface IVehicleAssignmentRepository
    {
        Task<bool> HasActiveAssignmentForVehicleAsync(
            Guid vehicleId,
            CancellationToken cancellationToken = default);

        Task<bool> HasActiveAssignmentForDriverAsync(
            Guid driverId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            VehicleAssignment assignment,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
