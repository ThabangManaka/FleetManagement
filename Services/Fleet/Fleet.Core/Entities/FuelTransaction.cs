namespace Fleet.Core.Entities;



    public class FuelTransaction
    {
        public Guid Id { get; private set; }

        public Guid VehicleId { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public int Mileage { get; private set; }

        public decimal Litres { get; private set; }

        public decimal PricePerLitre { get; private set; }

        public decimal TotalCost { get; private set; }

        public string FuelType { get; private set; } = string.Empty;

        public string? FuelStation { get; private set; }

        public string? ReceiptNumber { get; private set; }

        public string? Notes { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private FuelTransaction()
        {
        }

        public FuelTransaction(
            Guid vehicleId,
            DateTime transactionDate,
            int mileage,
            decimal litres,
            decimal pricePerLitre,
            string fuelType,
            string? fuelStation,
            string? receiptNumber,
            string? notes)
        {
            Id = Guid.NewGuid();

            VehicleId = vehicleId;

            TransactionDate = transactionDate;

            Mileage = mileage;

            Litres = litres;

            PricePerLitre = pricePerLitre;

            TotalCost = litres * pricePerLitre;

            FuelType = fuelType;

            FuelStation = fuelStation;

            ReceiptNumber = receiptNumber;

            Notes = notes;

            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            DateTime transactionDate,
            int mileage,
            decimal litres,
            decimal pricePerLitre,
            string fuelType,
            string? fuelStation,
            string? receiptNumber,
            string? notes)
        {
            TransactionDate = transactionDate;

            Mileage = mileage;

            Litres = litres;

            PricePerLitre = pricePerLitre;

            TotalCost = litres * pricePerLitre;

            FuelType = fuelType;

            FuelStation = fuelStation;

            ReceiptNumber = receiptNumber;

            Notes = notes;

            UpdatedAt = DateTime.UtcNow;
        }
    }
