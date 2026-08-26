using Fleet.Core.Enums;

namespace Fleet.Application.Features.Drivers.DTOs
{
    public record UpdateDriverRequest(
    string EmployeeNumber,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string LicenseNumber,
    DateTime LicenseExpiryDate,
    DriverStatus Status
);
}
