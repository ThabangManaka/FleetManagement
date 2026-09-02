using Fleet.Application.Features.Vehicles.Commands;
using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using Fleet.Core.Enums;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Handlers
{
    public class CreateVehicleCommandHandler
        : IRequestHandler<CreateVehicleCommand, VehicleResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public CreateVehicleCommandHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResponse> Handle(
            CreateVehicleCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            var registrationExists =
                await _vehicleRepository.ExistsByRegistrationNumberAsync(
                    request.RegistrationNumber,
                    cancellationToken: cancellationToken);

            if (registrationExists)
            {
                throw new InvalidOperationException(
                    "A vehicle with this registration number already exists.");
            }

            var vinExists =
                await _vehicleRepository.ExistsByVinAsync(
                    request.Vin,
                    cancellationToken: cancellationToken);

            if (vinExists)
            {
                throw new InvalidOperationException(
                    "A vehicle with this VIN already exists.");
            }

            var vehicle = new Vehicle(
                request.RegistrationNumber,
                request.Vin,
                request.Make,
                request.Model,
                request.Year,
                (FuelType)request.FuelType);

            await _vehicleRepository.AddAsync(
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