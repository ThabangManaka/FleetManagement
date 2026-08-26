using Fleet.Core.Enums;

namespace Fleet.Application.Features.Drivers.DTOs
{
    public record DriverResponse(
    Guid Id,
    string EmployeeNumber,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string LicenseNumber,
    DateTime LicenseExpiryDate,
    DriverStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
}
