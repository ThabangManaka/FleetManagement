using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class DeleteFuelTransactionCommandHandler
       : IRequestHandler<DeleteFuelTransactionCommand>
    {
        private readonly IFuelTransactionRepository _fuelRepository;

        public DeleteFuelTransactionCommandHandler(
            IFuelTransactionRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        public async Task Handle(
            DeleteFuelTransactionCommand command,
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

            _fuelRepository.Delete(fuelTransaction);

            await _fuelRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}