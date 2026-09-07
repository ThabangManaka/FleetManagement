using static Fleet.Core.Entities.Driver;

namespace Fleet.Application.Interfaces;

public partial interface IVehicleRepository
{
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
}