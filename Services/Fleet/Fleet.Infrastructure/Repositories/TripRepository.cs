using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Fleet.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly FleetDbContext _context;

        public TripRepository(FleetDbContext context)
        {
            _context = context;
        }

        public async Task<Trip?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Trips
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<Trip>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Trips
                .OrderByDescending(x => x.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            Trip trip,
            CancellationToken cancellationToken = default)
        {
            await _context.Trips.AddAsync(
                trip,
                cancellationToken);
        }

        public void Update(Trip trip)
        {
            _context.Trips.Update(trip);
        }

        public void Delete(Trip trip)
        {
            _context.Trips.Remove(trip);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Trips
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
