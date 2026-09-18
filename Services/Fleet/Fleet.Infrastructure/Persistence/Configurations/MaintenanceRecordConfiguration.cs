using Fleet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infrastructure.Persistence.Configurations;

    public class MaintenanceRecordConfiguration
    : IEntityTypeConfiguration<MaintenanceRecord>
    {
        public void Configure(
            EntityTypeBuilder<MaintenanceRecord> builder)
        {
            builder.ToTable("MaintenanceRecords");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VehicleId)
                .IsRequired();

            builder.Property(x => x.MaintenanceDate)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Cost)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Mileage)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasIndex(x => x.VehicleId);

            builder.HasOne(x => x.Vehicle)
                 .WithMany(x => x.MaintenanceRecords)
                 .HasForeignKey(x => x.VehicleId)
                 .OnDelete(DeleteBehavior.Cascade);

    }
    }