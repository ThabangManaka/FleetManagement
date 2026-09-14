using static Fleet.Core.Entities.Driver;

namespace Fleet.Application.Interfaces;

public interface IFuelTransactionRepository
{
    Task<FuelTransaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<FuelTransaction>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        FuelTransaction fuelTransaction,
        CancellationToken cancellationToken = default);

    void Update(FuelTransaction fuelTransaction);

    void Delete(FuelTransaction fuelTransaction);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}