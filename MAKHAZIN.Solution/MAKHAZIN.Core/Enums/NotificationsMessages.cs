using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Enums
{
    public class NotificationsMessages
    {
        public const string NewBidTitle = "New Bid";
        public const string NewBidMessage = "{0} placed a bid of {1} on your auction.";

        public const string AuctionWonTitle = "Auction Won";
        public const string AuctionWonMessage = "Congratulations {0}, you won the auction for {1}!";

        public const string AuctionLostTitle = "Auction Lost";
        public const string AuctionLostMessage = "Sorry {0}, you lost the auction for {1}.";
    }
}
