using FluentValidation;
using MAKHAZIN.Application.Features.Bids.Commands;

namespace MAKHAZIN.Application.Validators.Bids
{
    public class PlaceBidCommandValidator : AbstractValidator<PlaceBidCommand>
    {
        public PlaceBidCommandValidator()
        {
            RuleFor(x => x.AuctionId)
                .GreaterThan(0).WithMessage("Auction ID must be a positive number");

            RuleFor(x => x.BidPrice)
                .GreaterThan(0).WithMessage("Bid price must be greater than 0");
        }
    }
}
