using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler
        : IRequestHandler<GetVehicleQuery, VehicleResponse?>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleQueryHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResponse?> Handle(
            GetVehicleQuery query,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                query.Id,
                cancellationToken);

            if (vehicle is null)
            {
                return null;
            }

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