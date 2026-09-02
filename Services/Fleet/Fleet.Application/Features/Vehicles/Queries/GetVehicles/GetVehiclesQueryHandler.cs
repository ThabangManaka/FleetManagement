using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryHandler
        : IRequestHandler<GetVehiclesQuery, IReadOnlyList<VehicleResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesQueryHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IReadOnlyList<VehicleResponse>> Handle(
            GetVehiclesQuery query,
            CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(
                cancellationToken);

            return vehicles
                .Select(vehicle => new VehicleResponse
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
                })
                .ToList();
        }
    }
}