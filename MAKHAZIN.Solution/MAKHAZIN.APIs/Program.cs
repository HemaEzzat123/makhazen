using MAKHAZIN.APIs.Extensions;
using Serilog;

namespace MAKHAZIN.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Configure Serilog
            SerilogExtensions.ConfigureSerilog();

            try
            {
                Log.Information("Starting MAKHAZIN API application");

                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();

                // Add services
                builder.Services.AddControllers();
                builder.Services.ConfigureApiValidation(builder.Environment);
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddSignalR();
                builder.Services.AddHttpContextAccessor();

                builder.Services.AddDatabaseContexts(builder.Configuration);
                builder.Services.AddMediatRWithBehaviors();
                builder.Services.AddApplicationServices(builder.Configuration);
                builder.Services.AddIdentityServices(builder.Configuration);
                builder.Services.AddCorsPolicy(new[] { "http://localhost:3000" });

                var app = builder.Build();

                // Apply migrations and seed data
                Log.Information("Applying database migrations...");
                await app.ApplyMigrationsAndSeedAsync();
                Log.Information("Database migrations and seeding completed");

                // Configure middleware pipeline
                app.UseApplicationMiddleware();

                Log.Information("MAKHAZIN API application started successfully");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
