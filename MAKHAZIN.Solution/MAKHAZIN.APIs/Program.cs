
using MAKHAZIN.APIs.Extensions;
using MAKHAZIN.APIs.Middlewares;
using MAKHAZIN.Core.Application.Features.Auth.Commands;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Repository.Data;
using MAKHAZIN.Repository.Identity;
using MAKHAZIN.Services.Auth.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Logging.ClearProviders();
            webApplicationBuilder.Logging.AddConsole();
            webApplicationBuilder.Services.AddHttpContextAccessor();

            webApplicationBuilder.Services.AddDbContext<MAKHAZINDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });
            webApplicationBuilder.Services.AddDbContext<MAKHAZINIdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });
            webApplicationBuilder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<RegisterCommandHandler>();
            });
            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);


            webApplicationBuilder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
                });
            });
            #endregion

            var app = webApplicationBuilder.Build();

            #region Update - Database
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<MAKHAZINDbContext>(); // ASK CLR for creating object from DbContext [Explicitly]
            var _identityDbContext = services.GetRequiredService<MAKHAZINIdentityDbContext>(); // ASK CLR for creating object from AppIdentityDbContext [Explicitly]

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();


            try
            {
                await _dbContext.Database.MigrateAsync(); // Update-Database
                await _identityDbContext.Database.MigrateAsync(); // Update-IdentityDatabase
                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await MAKHAZINIdentityDbContextSeed.SeedAsync(userManger, _dbContext);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error occured during the migration of the Database");
            }
            #endregion
            #region Seeding Roles

            await SeedRoles.SeedRoleAsync(services);

            #endregion
            #region Configure Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("CorsPolicy");
            #endregion

            app.Run();
        }
    }
}
