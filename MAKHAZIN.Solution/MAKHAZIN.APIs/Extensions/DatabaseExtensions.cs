using MAKHAZIN.Repository.Data;
using MAKHAZIN.Repository.Identity;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.APIs.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Adds database contexts for main and identity databases
        /// </summary>
        public static IServiceCollection AddDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MAKHAZINDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<MAKHAZINIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            return services;
        }

        /// <summary>
        /// Applies pending migrations and seeds data
        /// </summary>
        public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<MAKHAZINDbContext>();
            var identityDbContext = services.GetRequiredService<MAKHAZINIdentityDbContext>();

            await dbContext.Database.MigrateAsync();
            await identityDbContext.Database.MigrateAsync();

            var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<MAKHAZIN.Core.Entities.Identity.AppUser>>();
            await MAKHAZINIdentityDbContextSeed.SeedAsync(userManager, dbContext);

            await SeedRoles.SeedRoleAsync(services);
        }
    }
}
