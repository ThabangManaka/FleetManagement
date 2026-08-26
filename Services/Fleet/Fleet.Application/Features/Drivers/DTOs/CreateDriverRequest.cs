using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Drivers.DTOs
{
    public record CreateDriverRequest(
      string EmployeeNumber,
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber,
      string LicenseNumber,
      DateTime LicenseExpiryDate
  );
}
