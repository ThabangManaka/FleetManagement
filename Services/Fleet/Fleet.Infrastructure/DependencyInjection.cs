
using Fleet.Application;
using Fleet.Application.Interfaces;
using Fleet.Infrastructure.Persistence;
using Fleet.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Fleet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<FleetDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("FleetDb")));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped< IVehicleAssignmentRepository,VehicleAssignmentRepository>();

            return services;
        }
    }
}
