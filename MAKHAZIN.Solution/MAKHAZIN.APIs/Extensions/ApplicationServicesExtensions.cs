using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core;
using MAKHAZIN.Application.Features.Orders.Builder;
using MAKHAZIN.Core.Services.Contract;
using MAKHAZIN.Repository;
using MAKHAZIN.Services;
using MAKHAZIN.Services.Configurations;
using MAKHAZIN.Services.Helpers;
using MAKHAZIN.Services.Notifications;
using MAKHAZIN.Services.Orders.Builder;
using Microsoft.AspNetCore.Mvc;

namespace MAKHAZIN.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOrderBuilder, OrderBuilder>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserHelper, UserHelper>();
            //services.AddAutoMapper(typeof(MappingProfile));

            // Bind EmailSettings from configuration
            services.Configure<EmailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<FrontendSettings>(configuration.GetSection("FrontendSettings"));

            #region Error Handling

            services.Configure<ApiBehaviorOptions>(options =>
            {
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                     .SelectMany(e => e.Value.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToList();
                var response = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(response);
            };
        });


            #endregion

            return services;
        }
    }
}
