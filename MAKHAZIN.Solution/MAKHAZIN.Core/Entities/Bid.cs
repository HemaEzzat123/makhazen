using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Bid : BaseEntity
    {
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }

        public decimal BidPrice { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation
        public Auction Auction { get; set; }
        public User User { get; set; }
    }

}
