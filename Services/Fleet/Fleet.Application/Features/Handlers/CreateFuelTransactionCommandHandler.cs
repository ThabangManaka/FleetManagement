using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using MediatR;



namespace Fleet.Application.Features.Handlers
{
    public class CreateFuelTransactionCommandHandler
        : IRequestHandler<CreateFuelTransactionCommand, Guid>
    {
        private readonly IFuelTransactionRepository _fuelRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CreateFuelTransactionCommandHandler(
            IFuelTransactionRepository fuelRepository,
            IVehicleRepository vehicleRepository)
        {
            _fuelRepository = fuelRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Guid> Handle(
            CreateFuelTransactionCommand command,
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

            if (command.Request.Mileage < vehicle.Mileage)
            {
                throw new InvalidOperationException(
                    $"Fuel transaction mileage ({command.Request.Mileage}) " +
                    $"cannot be less than the vehicle's current mileage ({vehicle.Mileage}).");
            }

            var fuelTransaction = new FuelTransaction(
                command.Request.VehicleId,
                command.Request.TransactionDate,
                command.Request.Mileage,
                command.Request.Litres,
                command.Request.PricePerLitre,
                command.Request.FuelType,
                command.Request.FuelStation,
                command.Request.ReceiptNumber,
                command.Request.Notes);

            await _fuelRepository.AddAsync(
                fuelTransaction,
                cancellationToken);

            await _fuelRepository.SaveChangesAsync(
                cancellationToken);

            return fuelTransaction.Id;
        }
    }
}
