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
    }
}