using Fleet.API.Middleware;
using Fleet.Application;
using Fleet.Infrastructure;
using Fleet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/fleet-api-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true)
    .CreateLogger();


try
{
    Log.Information("Starting Fleet API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddApplication();

    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode =
            StatusCodes.Status429TooManyRequests;

        options.AddFixedWindowLimiter("FleetApi", limiter =>
        {
            limiter.PermitLimit = 100;
            limiter.Window = TimeSpan.FromMinutes(1);
            limiter.QueueLimit = 0;
            limiter.AutoReplenishment = true;
        });
    });

    builder.Services.AddInfrastructure(
        builder.Configuration);

    builder.Services.AddControllers();

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddHealthChecks();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AngularClient", policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    var app = builder.Build();

    app.UseExceptionHandler();

    app.UseMiddleware<SecurityHeadersMiddleware>();

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors("AngularClient");

    app.UseRateLimiter();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

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