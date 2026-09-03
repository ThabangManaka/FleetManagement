using Fleet.Core.Entities;

namespace Fleet.Application.Interfaces
{
    public interface IDriverRepository
    {
        Task<Driver?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Driver>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Driver driver,
            CancellationToken cancellationToken = default);

        void Update(Driver driver);

        void Delete(Driver driver);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}