
namespace Fleet.Application.Features.Vehicles.DTOs
{
    public class UpdateVehicleRequest
    {
        public string RegistrationNumber { get; set; } = string.Empty;

        public string Vin { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public int FuelType { get; set; }

        public int Status { get; set; }

        public decimal Mileage { get; set; }
    }
}
