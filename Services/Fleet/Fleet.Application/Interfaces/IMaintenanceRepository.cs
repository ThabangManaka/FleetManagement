using Fleet.Core.Entities;

namespace Fleet.Application.Interfaces;

    public interface IMaintenanceRepository
    {
        Task<Maintenance?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Maintenance>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Maintenance maintenance,
            CancellationToken cancellationToken = default);

        void Update(Maintenance maintenance);

        void Delete(Maintenance maintenance);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
