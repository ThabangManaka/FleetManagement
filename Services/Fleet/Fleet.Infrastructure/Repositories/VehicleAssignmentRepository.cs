using Fleet.Application;
using Fleet.Core.Entities;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Fleet.Infrastructure.Repositories
{
    public class VehicleAssignmentRepository : IVehicleAssignmentRepository
    {
        private readonly FleetDbContext _context;

        public VehicleAssignmentRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasActiveAssignmentForVehicleAsync(
            Guid vehicleId,
            CancellationToken cancellationToken = default)
        {
            return await _context.VehicleAssignments
                .AnyAsync(
                    x => x.VehicleId == vehicleId &&
                         x.UnassignedAt == null,
                    cancellationToken);
        }

        public async Task<bool> HasActiveAssignmentForDriverAsync(
            Guid driverId,
            CancellationToken cancellationToken = default)
        {
            return await _context.VehicleAssignments
                .AnyAsync(
                    x => x.DriverId == driverId &&
                         x.UnassignedAt == null,
                    cancellationToken);
        }

        public async Task AddAsync(
            VehicleAssignment assignment,
            CancellationToken cancellationToken = default)
        {
            await _context.VehicleAssignments.AddAsync(
                assignment,
                cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
