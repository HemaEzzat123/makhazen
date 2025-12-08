using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Application.Features.Bids.Commands
{
    public class PlaceBidCommand : ICommand<BidDTO>
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal BidPrice { get; set; }
    }
}
