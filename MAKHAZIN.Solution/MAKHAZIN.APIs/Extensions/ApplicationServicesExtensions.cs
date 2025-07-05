using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core;
using MAKHAZIN.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MAKHAZIN.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddAutoMapper(typeof(MappingProfile));

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
