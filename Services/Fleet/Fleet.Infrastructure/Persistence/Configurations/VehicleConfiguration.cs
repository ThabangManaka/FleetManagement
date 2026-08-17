using Fleet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.RegistrationNumber)
            .IsUnique();

        builder.Property(x => x.Vin)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Vin)
            .IsUnique();

        builder.Property(x => x.Make)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FuelType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Mileage)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .IsRequired();
    }
}