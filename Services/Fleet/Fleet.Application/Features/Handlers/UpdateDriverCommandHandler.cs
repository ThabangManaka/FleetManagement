using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class UpdateDriverCommandHandler
        : IRequestHandler<UpdateDriverCommand>
    {
        private readonly IDriverRepository _driverRepository;

        public UpdateDriverCommandHandler(
            IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task Handle(
            UpdateDriverCommand command,
            CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (driver is null)
            {
                throw new KeyNotFoundException(
                    $"Driver with ID '{command.Id}' was not found.");
            }

            driver.UpdateDetails(
                command.Request.EmployeeNumber,
                command.Request.FirstName,
                command.Request.LastName,
                command.Request.Email,
                command.Request.PhoneNumber,
                command.Request.LicenseNumber,
                command.Request.LicenseExpiryDate,
                command.Request.Status);

            _driverRepository.Update(driver);

            await _driverRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}