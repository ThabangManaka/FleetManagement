using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly FleetDbContext _context;

        public MaintenanceRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<Maintenance?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Maintenances
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<Maintenance>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Maintenances
                .OrderByDescending(x => x.ServiceDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            Maintenance maintenance,
            CancellationToken cancellationToken = default)
        {
            await _context.Maintenances.AddAsync(
                maintenance,
                cancellationToken);
        }

        public void Update(Maintenance maintenance)
        {
            _context.Maintenances.Update(maintenance);
        }

        public void Delete(Maintenance maintenance)
        {
            _context.Maintenances.Remove(maintenance);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Maintenances
                .AnyAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}