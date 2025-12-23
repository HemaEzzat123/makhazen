using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Core.Sepecification
{
    /// <summary>
    /// Specification to get bids for a specific auction with User navigation property loaded
    /// </summary>
    public class BidsForAuctionWithUserSpecification : BaseSepecification<Bid>
    {
        public BidsForAuctionWithUserSpecification(int auctionId)
            : base(b => b.AuctionId == auctionId)
        {
            Includes.Add(b => b.User);
            AddOrderByDesc(b => b.BidPrice);
        }
    }
}
