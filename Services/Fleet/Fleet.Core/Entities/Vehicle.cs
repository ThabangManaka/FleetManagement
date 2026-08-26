using Fleet.Core.Enums;

namespace Fleet.Core.Entities;

public partial class Vehicle
{
    public Guid Id { get; private set; }

    public string RegistrationNumber { get; private set; } = string.Empty;

    public string Vin { get; private set; } = string.Empty;

    public string Make { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public int Year { get; private set; }

    public FuelType FuelType { get; private set; }

    public decimal Mileage { get; private set; }

    public VehicleStatus Status { get; private set; }

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
        FuelType fuelType)
    {
        Id = Guid.NewGuid();
        RegistrationNumber = registrationNumber;
        Vin = vin;
        Make = make;
        Model = model;
        Year = year;
        FuelType = fuelType;
        Mileage = 0;
        Status = VehicleStatus.Available;
        CreatedAt = DateTime.UtcNow;
    }
    public void UpdateDetails(
    string registrationNumber,
    string vin,
    string make,
    string model,
    int year,
    FuelType fuelType,
    VehicleStatus status,
    decimal mileage)
    {
        RegistrationNumber = registrationNumber;
        Vin = vin;
        Make = make;
        Model = model;
        Year = year;
        FuelType = fuelType;
        Status = status;
        Mileage = mileage;
        UpdatedAt = DateTime.UtcNow;
    }
    }