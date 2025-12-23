using FluentValidation;
using MAKHAZIN.Application.CQRS;
using MediatR;

namespace MAKHAZIN.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errorMessages = string.Join("; ", failures.Select(f => f.ErrorMessage));
                
                // Check if TResponse is a Result type and return a failure
                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var innerType = responseType.GetGenericArguments()[0];
                    var failureMethod = typeof(Result<>).MakeGenericType(innerType).GetMethod("Failure", new[] { typeof(string) });
                    return (TResponse)failureMethod!.Invoke(null, new object[] { errorMessages })!;
                }

                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
