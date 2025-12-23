using MAKHAZIN.APIs.Middlewares;
using MAKHAZIN.Core.Hubs.Notifications;

namespace MAKHAZIN.APIs.Extensions
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configures all application middleware in the correct order
        /// </summary>
        public static WebApplication UseApplicationMiddleware(this WebApplication app)
        {
            // Request logging
            app.UseSerilogRequestLoggingWithContext();

            // Exception handling
            app.UseMiddleware<ExceptionMiddleware>();

            // Swagger (dev only)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Error pages
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Static files and HTTPS
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Endpoints
            app.MapControllers();
            app.MapHub<NotificationHub>("/hubs/notifications");

            // CORS
            app.UseCors("CorsPolicy");

            return app;
        }

        /// <summary>
        /// Adds CORS policy for frontend
        /// </summary>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string[] allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithOrigins(allowedOrigins);
                });
            });

            return services;
        }
    }
}
