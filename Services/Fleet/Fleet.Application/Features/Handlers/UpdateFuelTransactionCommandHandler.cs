using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class UpdateFuelTransactionCommandHandler
        : IRequestHandler<UpdateFuelTransactionCommand>
    {
        private readonly IFuelTransactionRepository _fuelRepository;

        public UpdateFuelTransactionCommandHandler(
            IFuelTransactionRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task Handle(
            UpdateFuelTransactionCommand command,
            CancellationToken cancellationToken)
        {
            var fuelTransaction = await _fuelRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (fuelTransaction is null)
            {
                throw new KeyNotFoundException(
                    $"Fuel transaction with ID '{command.Id}' was not found.");
            }

            fuelTransaction.UpdateDetails(
                command.Request.TransactionDate,
                command.Request.Mileage,
                command.Request.Litres,
                command.Request.PricePerLitre,
                command.Request.FuelType,
                command.Request.FuelStation,
                command.Request.ReceiptNumber,
                command.Request.Notes);

            _fuelRepository.Update(fuelTransaction);

            await _fuelRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}