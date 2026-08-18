using Fleet.Application.Features.Vehicles.Handlers;
using Fleet.Application.Features.Vehicles.Queries.GetVehicle;
using Fleet.Application.Features.Vehicles.Queries.GetVehicles;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Fleet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly());

            services.AddScoped<CreateVehicleCommandHandler>();
            services.AddScoped<UpdateVehicleCommandHandler>();
            services.AddScoped<DeleteVehicleCommandHandler>();

            services.AddScoped<GetVehicleQueryHandler>();
            services.AddScoped<GetVehiclesQueryHandler>();

            return services;
        }


    }
}
