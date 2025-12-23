using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auctions.Commands
{
    public class UpdateAuctionCommand : ICommand<bool>
    {
        public int AuctionId { get; set; }
        public decimal? StartingPrice { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}
