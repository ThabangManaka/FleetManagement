using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class CreateTripCommandHandler
            : IRequestHandler<CreateTripCommand, Guid>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;

        public CreateTripCommandHandler(
            ITripRepository tripRepository,
            IVehicleRepository vehicleRepository,
            IDriverRepository driverRepository)
        {
            _tripRepository = tripRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }

        public async Task<Guid> Handle(
            CreateTripCommand command,
            CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(
                command.Request.VehicleId,
                cancellationToken);

            if (vehicle is null)
            {
                throw new KeyNotFoundException(
                    $"Vehicle with ID '{command.Request.VehicleId}' was not found.");
            }

            var driver = await _driverRepository.GetByIdAsync(
                command.Request.DriverId,
                cancellationToken);

            if (driver is null)
            {
                throw new KeyNotFoundException(
                    $"Driver with ID '{command.Request.DriverId}' was not found.");
            }

            var trip = new Fleet.Core.Entities.Trip(
                command.Request.VehicleId,
                command.Request.DriverId,
                command.Request.StartLocation,
                command.Request.Destination,
                command.Request.StartDate,
                command.Request.StartMileage,
                command.Request.Notes);

            await _tripRepository.AddAsync(
                trip,
                cancellationToken);

            await _tripRepository.SaveChangesAsync(
                cancellationToken);

            return trip.Id;
        }
    }
}