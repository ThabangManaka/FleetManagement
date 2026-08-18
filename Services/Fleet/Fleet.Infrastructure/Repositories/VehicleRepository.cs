using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Fleet.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FleetDbContext _context;

        public VehicleRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Vehicle>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .OrderBy(x => x.RegistrationNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            Vehicle vehicle,
            CancellationToken cancellationToken = default)
        {
            await _context.Vehicles.AddAsync(
                vehicle,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            Vehicle vehicle,
            CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Update(vehicle);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(
            Vehicle vehicle,
            CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Remove(vehicle);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByRegistrationNumberAsync(
            string registrationNumber,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AnyAsync(
                    x => x.RegistrationNumber == registrationNumber &&
                         (!excludeId.HasValue || x.Id != excludeId.Value),
                    cancellationToken);
        }

        public async Task<bool> ExistsByVinAsync(
            string vin,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AnyAsync(
                    x => x.Vin == vin &&
                         (!excludeId.HasValue || x.Id != excludeId.Value),
                    cancellationToken);
        }
    }
}
