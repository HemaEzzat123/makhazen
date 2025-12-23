using FluentValidation;
using MAKHAZIN.Application.Features.Products.Commands;

namespace MAKHAZIN.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MinimumLength(2).WithMessage("Product name must be at least 2 characters")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters");

            RuleFor(x => x.UnitOfMeasurement)
                .NotEmpty().WithMessage("Unit of measurement is required")
                .MaximumLength(50).WithMessage("Unit of measurement must not exceed 50 characters");
        }
    }
}
