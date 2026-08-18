
namespace Fleet.Application.Features.Vehicles.DTOs
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Vin { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string FuelType { get; set; } = string.Empty;

        public decimal Mileage { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
