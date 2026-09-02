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
            // MediatR - automatically discovers all handlers
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    typeof(AssemblyReference).Assembly));

            // FluentValidation
            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly());

            return services;
        }
    }

}
