using Fleet.Application;
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

        public async Task<Driver?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default)
        {
            return await _context.Drivers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _context.Drivers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Driver driver)
        {
            await _context.Drivers.AddAsync(driver);
        }

        public void Update(Driver driver)
        {
            _context.Drivers.Update(driver);
        }

        public void Delete(Driver driver)
        {
            _context.Drivers.Remove(driver);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Drivers
                .AnyAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}