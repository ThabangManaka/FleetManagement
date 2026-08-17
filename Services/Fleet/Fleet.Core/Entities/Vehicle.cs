namespace Fleet.Core.Entities;

public class Vehicle
{
    public Guid Id { get; private set; }

    public string RegistrationNumber { get; private set; } = string.Empty;

    public string Vin { get; private set; } = string.Empty;

    public string Make { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public int Year { get; private set; }

    public string FuelType { get; private set; } = string.Empty;

    public decimal Mileage { get; private set; }

    public string Status { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private Vehicle()
    {
    }

    public Vehicle(
        string registrationNumber,
        string vin,
        string make,
        string model,
        int year,
        string fuelType)
    {
        Id = Guid.NewGuid();
        RegistrationNumber = registrationNumber;
        Vin = vin;
        Make = make;
        Model = model;
        Year = year;
        FuelType = fuelType;
        Mileage = 0;
        Status = "Available";
        CreatedAt = DateTime.UtcNow;
    }
}