using FluentValidation;
using MAKHAZIN.Application.Features.Orders.Command;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Validators.Orders
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .GreaterThan(0).WithMessage("Seller ID must be a positive number");

            RuleFor(x => x.OrderItems)
                .NotNull().WithMessage("Order items are required")
                .NotEmpty().WithMessage("Order must have at least one item");

            RuleForEach(x => x.OrderItems).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .GreaterThan(0).WithMessage("Product ID must be a positive number");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0");

                item.RuleFor(x => x.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative");

                item.RuleFor(x => x.Discount)
                    .InclusiveBetween(0, 1).WithMessage("Discount must be between 0 and 1 (0% to 100%)");
            });
        }
    }
}
