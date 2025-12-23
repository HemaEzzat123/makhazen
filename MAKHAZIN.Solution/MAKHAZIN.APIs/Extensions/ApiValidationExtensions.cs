using MAKHAZIN.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MAKHAZIN.APIs.Extensions
{
    public static class ApiValidationExtensions
    {
        /// <summary>
        /// Configures API validation to return secure, user-friendly error messages
        /// </summary>
        public static IServiceCollection ConfigureApiValidation(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(e => e.Value!.Errors.Select(error => 
                            SanitizeErrorMessage(error.ErrorMessage, e.Key, environment.IsDevelopment())))
                        .ToList();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }

        /// <summary>
        /// Sanitizes error messages to hide sensitive information in production
        /// </summary>
        private static string SanitizeErrorMessage(string errorMessage, string fieldName, bool isDevelopment)
        {
            // In development, show full details for debugging
            if (isDevelopment)
            {
                return errorMessage;
            }

            // In production, sanitize error messages
            // Hide JSON parsing details, paths, line numbers
            if (errorMessage.Contains("JSON value could not be converted") ||
                errorMessage.Contains("LineNumber") ||
                errorMessage.Contains("BytePositionInLine") ||
                errorMessage.Contains("Path:"))
            {
                // Provide a user-friendly message based on the field name
                return $"The value provided for '{GetFriendlyFieldName(fieldName)}' is invalid";
            }

            // Hide internal exception details
            if (errorMessage.Contains("Exception") || 
                errorMessage.Contains("StackTrace") ||
                errorMessage.Contains("at ") && errorMessage.Contains("in "))
            {
                return "An error occurred while processing your request";
            }

            // For validation errors, return as-is (they are user-friendly)
            return errorMessage;
        }

        /// <summary>
        /// Converts field names to user-friendly format
        /// </summary>
        private static string GetFriendlyFieldName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || fieldName == "$")
                return "request body";

            // Convert camelCase/PascalCase to readable format
            // e.g., "expirationTime" -> "Expiration Time"
            var result = string.Concat(
                fieldName.Select((c, i) => 
                    i > 0 && char.IsUpper(c) ? " " + c : c.ToString()));
            
            return char.ToUpper(result[0]) + result.Substring(1);
        }
    }
}
