using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Core.Sepecification
{
    public class AuctionWithBidsSpecification : BaseSepecification<Auction>
    {
        public AuctionWithBidsSpecification(int auctionId)
            : base(a => a.Id == auctionId)
        {
            Includes.Add(a => a.Bids);
            Includes.Add(a => a.User);
            Includes.Add(a => a.Product);
            AddInclude("Bids.User"); // Include User for each Bid
        }
    }
}
