using FluentValidation;
using MAKHAZIN.Application.Features.Auctions.Commands;

namespace MAKHAZIN.Application.Validators.Auctions
{
    public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
    {
        public CreateAuctionCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be a positive number");

            RuleFor(x => x.StartingPrice)
                .GreaterThan(0).WithMessage("Starting price must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.ExpirationTime)
                .GreaterThan(DateTime.UtcNow.AddHours(1)).WithMessage("Expiration time must be at least 1 hour in the future")
                .LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("Expiration time must not exceed 1 year from now");
        }
    }
}
