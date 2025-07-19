using MAKHAZIN.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Bid : BaseEntity
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }

        public decimal BidPrice { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation
        public Auction Auction { get; set; }
        public User User { get; set; }
    }

}
