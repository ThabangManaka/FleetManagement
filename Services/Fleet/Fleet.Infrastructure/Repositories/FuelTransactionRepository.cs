using Fleet.Application.Interfaces;
using Fleet.Infrastructure.Persistence;
using static Fleet.Core.Entities.Driver;

namespace Fleet.Infrastructure.Repositories
{
    public class FuelTransactionRepository
          : IFuelTransactionRepository
    {
        private readonly FleetDbContext _context;

        public FuelTransactionRepository(
            FleetDbContext context)
        {
            _context = context;
        }

        public async Task<FuelTransaction?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.FuelTransactions
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<FuelTransaction>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.FuelTransactions
                .OrderByDescending(x => x.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            FuelTransaction fuelTransaction,
            CancellationToken cancellationToken = default)
        {
            await _context.FuelTransactions.AddAsync(
                fuelTransaction,
                cancellationToken);
        }

        public void Update(FuelTransaction fuelTransaction)
        {
            _context.FuelTransactions.Update(fuelTransaction);
        }

        public void Delete(FuelTransaction fuelTransaction)
        {
            _context.FuelTransactions.Remove(fuelTransaction);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.FuelTransactions
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
