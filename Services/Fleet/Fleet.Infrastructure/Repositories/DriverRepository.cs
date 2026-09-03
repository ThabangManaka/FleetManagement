using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly FleetDbContext _context;

        public DriverRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<Driver?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Drivers
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<Driver>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Drivers
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            Driver driver,
            CancellationToken cancellationToken = default)
        {
            await _context.Drivers.AddAsync(
                driver,
                cancellationToken);
        }

        public void Update(Driver driver)
        {
            _context.Drivers.Update(driver);
        }

        public void Delete(Driver driver)
        {
            _context.Drivers.Remove(driver);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Drivers
                .AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}