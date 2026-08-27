using Fleet.Application.Features.Commands;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class DeleteDriverCommandHandler
    : IRequestHandler<DeleteDriverCommand>
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverCommandHandler(
            IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task Handle(
            DeleteDriverCommand command,
            CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(command.Id);

            if (driver is null)
            {
                throw new KeyNotFoundException(
                    $"Driver with ID '{command.Id}' was not found.");
            }

            _driverRepository.Delete(driver);

            await _driverRepository.SaveChangesAsync();
        }
    }
}
