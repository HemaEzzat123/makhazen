using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Bids.Queries
{
    public class GetHighestBidQuery : IQuery<BidDTO>
    {
        public int AuctionId { get; set; }
        public GetHighestBidQuery(int auctionId) => AuctionId = auctionId;
    }
}
