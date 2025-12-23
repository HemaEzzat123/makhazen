using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Bids.Commands
{
    public class PlaceBidCommand : ICommand<BidDTO>
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal BidPrice { get; set; }
    }
}
