using FluentValidation;
using MAKHAZIN.Application.Features.Auth.Commands;

namespace MAKHAZIN.Application.Validators.Auth
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
