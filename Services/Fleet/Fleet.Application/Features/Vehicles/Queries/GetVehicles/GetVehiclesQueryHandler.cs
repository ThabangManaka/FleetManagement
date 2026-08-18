using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Vehicles.Queries.GetVehicles
{
    public class GetVehiclesQueryHandler
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesQueryHandler(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IReadOnlyList<VehicleResponse>> HandleAsync(
            GetVehiclesQuery query,
            CancellationToken cancellationToken = default)
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
