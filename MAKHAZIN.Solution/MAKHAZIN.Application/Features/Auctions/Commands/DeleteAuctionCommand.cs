using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auctions.Commands
{
    public class DeleteAuctionCommand : ICommand<bool>
    {
        public int AuctionId { get; set; }
    }
}
