using Fleet.Application.Features.Vehicles.Commands;
using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using Fleet.Core.Enums;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class UpdateVehicleCommandHandler
    {
        private readonly IVehicleRepository _vehicleRepository;

        public UpdateVehicleCommandHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResponse> HandleAsync(
            UpdateVehicleCommand command,
            CancellationToken cancellationToken = default)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (vehicle == null)
            {
                throw new KeyNotFoundException(
                    "Vehicle not found.");
            }

            var request = command.Request;

            var registrationExists =
                await _vehicleRepository.ExistsByRegistrationNumberAsync(
                    request.RegistrationNumber,
                    command.Id,
                    cancellationToken);

            if (registrationExists)
            {
                throw new InvalidOperationException(
                    "A vehicle with this registration number already exists.");
            }

            var vinExists =
                await _vehicleRepository.ExistsByVinAsync(
                    request.Vin,
                    command.Id,
                    cancellationToken);

            if (vinExists)
            {
                throw new InvalidOperationException(
                    "A vehicle with this VIN already exists.");
            }

            vehicle.UpdateDetails(
                request.RegistrationNumber,
                request.Vin,
                request.Make,
                request.Model,
                request.Year,
                (FuelType)request.FuelType,
                (VehicleStatus)request.Status,
                request.Mileage);

            await _vehicleRepository.UpdateAsync(
                vehicle,
                cancellationToken);

            return new VehicleResponse
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Vin = vehicle.Vin,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                FuelType = vehicle.FuelType.ToString(),
                Mileage = vehicle.Mileage,
                Status = vehicle.Status.ToString(),
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };
        }
    }
}
