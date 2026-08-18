using Fleet.Core.Entities;

namespace Fleet.Application.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Vehicle>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Vehicle vehicle,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Vehicle vehicle,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Vehicle vehicle,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByRegistrationNumberAsync(
        string registrationNumber,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByVinAsync(
        string vin,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);
}