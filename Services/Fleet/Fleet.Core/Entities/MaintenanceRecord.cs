namespace Fleet.Core.Entities
{
    public class MaintenanceRecord
    {
        public Guid Id { get; private set; }

        public Guid VehicleId { get; private set; }

        public DateTime MaintenanceDate { get; private set; }

        public string Description { get; private set; } = string.Empty;

        public decimal Cost { get; private set; }

        public int Mileage { get; private set; }

        private MaintenanceRecord()
        {
        }

        public MaintenanceRecord(
            Guid vehicleId,
            DateTime maintenanceDate,
            string description,
            decimal cost,
            int mileage)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            MaintenanceDate = maintenanceDate;
            Description = description;
            Cost = cost;
            Mileage = mileage;
        }
    }
}
