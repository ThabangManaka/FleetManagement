using Fleet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infrastructure.Persistence;

public class FleetDbContext : DbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public DbSet<Driver> Drivers => Set<Driver>();

    public DbSet<VehicleAssignment> VehicleAssignments
    => Set<VehicleAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {  

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FleetDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>()
        .Property(x => x.FuelType)
        .HasConversion<string>();

        modelBuilder.Entity<Vehicle>()
            .Property(x => x.Status)
            .HasConversion<string>();

        modelBuilder.Entity<VehicleAssignment>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.Vehicle)
                .WithMany()
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Driver)
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.AssignedAt)
                .IsRequired();

            entity.Property(x => x.UnassignedAt)
                .IsRequired(false);
        });
    }
}