using Fleet.Core.Enums;

namespace Fleet.Core.Entities;

    public class Maintenance
    {
        public Guid Id { get; private set; }

        public Guid VehicleId { get; private set; }

        public string MaintenanceType { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public DateTime ServiceDate { get; private set; }

        public int Mileage { get; private set; }

        public decimal Cost { get; private set; }

        public string? Notes { get; private set; }

        public MaintenanceStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private Maintenance()
        {
        }

        public Maintenance(
            Guid vehicleId,
            string maintenanceType,
            string description,
            DateTime serviceDate,
            int mileage,
            decimal cost,
            string? notes)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            MaintenanceType = maintenanceType;
            Description = description;
            ServiceDate = serviceDate;
            Mileage = mileage;
            Cost = cost;
            Notes = notes;
            Status = MaintenanceStatus.Scheduled;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            string maintenanceType,
            string description,
            DateTime serviceDate,
            int mileage,
            decimal cost,
            string? notes,
            MaintenanceStatus status)
        {
            MaintenanceType = maintenanceType;
            Description = description;
            ServiceDate = serviceDate;
            Mileage = mileage;
            Cost = cost;
            Notes = notes;
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }
    }

