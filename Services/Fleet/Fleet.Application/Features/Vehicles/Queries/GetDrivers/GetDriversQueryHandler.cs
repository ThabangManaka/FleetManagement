using Fleet.Application.Features.Drivers.DTOs;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Vehicles.Queries.GetDrivers
{
    public class GetDriversQueryHandler
        : IRequestHandler<GetDriversQuery, List<DriverResponse>>
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriversQueryHandler(
            IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<List<DriverResponse>> Handle(
            GetDriversQuery query,
            CancellationToken cancellationToken)
        {
            var drivers = await _driverRepository.GetAllAsync(
                cancellationToken);

            return drivers
                .Select(driver => new DriverResponse(
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
                    driver.UpdatedAt))
                .ToList();
        }
    }
}