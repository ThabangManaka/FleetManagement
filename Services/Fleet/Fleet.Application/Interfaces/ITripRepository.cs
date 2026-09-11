using Fleet.Core.Entities;

namespace Fleet.Application.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Trip>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Trip trip,
            CancellationToken cancellationToken = default);

        void Update(Trip trip);

        void Delete(Trip trip);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
