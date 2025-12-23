using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Bids.Queries
{
    public class GetBidsForAuctionQuery : IQuery<List<BidDTO>>
    {
        public int AuctionId { get; set; }
        public GetBidsForAuctionQuery(int auctionId) => AuctionId = auctionId;
    }
}
