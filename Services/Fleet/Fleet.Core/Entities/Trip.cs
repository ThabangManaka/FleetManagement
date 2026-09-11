using Fleet.Core.Enums;

namespace Fleet.Core.Entities
{
    public class Trip
    {
        public Guid Id { get; private set; }

        public Guid VehicleId { get; private set; }

        public Guid DriverId { get; private set; }

        public string StartLocation { get; private set; } = string.Empty;

        public string Destination { get; private set; } = string.Empty;

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public int StartMileage { get; private set; }

        public int? EndMileage { get; private set; }

        public TripStatus Status { get; private set; }

        public string? Notes { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private Trip()
        {
        }

        public Trip(
            Guid vehicleId,
            Guid driverId,
            string startLocation,
            string destination,
            DateTime startDate,
            int startMileage,
            string? notes)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            DriverId = driverId;
            StartLocation = startLocation;
            Destination = destination;
            StartDate = startDate;
            StartMileage = startMileage;
            Notes = notes;
            Status = TripStatus.Planned;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            string startLocation,
            string destination,
            DateTime startDate,
            DateTime? endDate,
            int startMileage,
            int? endMileage,
            TripStatus status,
            string? notes)
        {
            StartLocation = startLocation;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
            StartMileage = startMileage;
            EndMileage = endMileage;
            Status = status;
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
