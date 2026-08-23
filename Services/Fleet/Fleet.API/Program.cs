using Fleet.API.Middleware;
using Fleet.Application;
using Fleet.Infrastructure;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/fleet-api-.log",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


try
{
    Log.Information("Starting Fleet API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(
        builder.Configuration);

    builder.Services.AddControllers();

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddHealthChecks();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseExceptionHandler();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.MapHealthChecks("/health");

    app.MapControllers();

    Log.Information("Fleet API started successfully");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fleet API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}