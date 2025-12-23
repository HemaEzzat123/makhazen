using FluentValidation;
using MAKHAZIN.Application.Behaviors;
using MAKHAZIN.Application.Validators.Auth;
using MAKHAZIN.Services.Auth.Commands;
using MediatR;

namespace MAKHAZIN.APIs.Extensions
{
    public static class MediatRExtensions
    {
        /// <summary>
        /// Adds MediatR with handlers, validators, and pipeline behaviors
        /// </summary>
        public static IServiceCollection AddMediatRWithBehaviors(this IServiceCollection services)
        {
            // Register MediatR with handlers from Services assembly
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<RegisterCommandHandler>();
            });

            // Register FluentValidation validators from Core assembly
            services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

            // Register pipeline behaviors (order matters: Validation runs before Logging)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
