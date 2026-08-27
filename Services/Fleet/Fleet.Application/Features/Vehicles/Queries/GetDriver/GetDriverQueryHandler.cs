using Fleet.Application.Features.Drivers.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetDriver
{
    public class GetDriverQueryHandler
    : IRequestHandler<GetDriverQuery, DriverResponse?>
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverQueryHandler(
            IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<DriverResponse?> Handle(
            GetDriverQuery query,
            CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(query.Id);

            if (driver is null)
            {
                return null;
            }

            return new DriverResponse(
                driver.Id,
                driver.EmployeeNumber,
                driver.FirstName,
                driver.LastName,
                driver.Email,
                driver.PhoneNumber,
                driver.LicenseNumber,
                driver.LicenseExpiryDate,
                driver.Status,
                driver.CreatedAt,
                driver.UpdatedAt);
        }
    }
}
