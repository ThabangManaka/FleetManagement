using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using Fleet.Core.Entities;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class CreateDriverCommandHandler
        : IRequestHandler<CreateDriverCommand, Guid>
    {
        private readonly IDriverRepository _driverRepository;

        public CreateDriverCommandHandler(
            IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Guid> Handle(
            CreateDriverCommand command,
            CancellationToken cancellationToken)
        {
            var driver = new Driver(
                command.Request.EmployeeNumber,
                command.Request.FirstName,
                command.Request.LastName,
                command.Request.Email,
                command.Request.PhoneNumber,
                command.Request.LicenseNumber,
                command.Request.LicenseExpiryDate);

            await _driverRepository.AddAsync(
                driver,
                cancellationToken);

            await _driverRepository.SaveChangesAsync(
                cancellationToken);

            return driver.Id;
        }
    }
}