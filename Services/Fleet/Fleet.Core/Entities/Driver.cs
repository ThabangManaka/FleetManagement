using Fleet.Core.Enums;

namespace Fleet.Core.Entities;


    public class Driver
    {
        public Guid Id { get; private set; }

        public string EmployeeNumber { get; private set; } = string.Empty;

        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string PhoneNumber { get; private set; } = string.Empty;

        public string LicenseNumber { get; private set; } = string.Empty;

        public DateTime LicenseExpiryDate { get; private set; }

        public DriverStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private Driver()
        {
        }

        public Driver(
            string employeeNumber,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string licenseNumber,
            DateTime licenseExpiryDate)
        {
            Id = Guid.NewGuid();
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            LicenseNumber = licenseNumber;
            LicenseExpiryDate = licenseExpiryDate;
            Status = DriverStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            string employeeNumber,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string licenseNumber,
            DateTime licenseExpiryDate,
            DriverStatus status)
        {
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            LicenseNumber = licenseNumber;
            LicenseExpiryDate = licenseExpiryDate;
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }
    }
