using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class User : BaseEntity
    {
        public string ExternalId { get; set; } // For User in the identity Db
        public string Name { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }


        // Navigation Properties
        public ICollection<StockItem> StockItems { get; set; }
        public ICollection<Order> OrdersPlaced { get; set; }        // As Buyer
        public ICollection<Order> OrdersReceived { get; set; }      // As Seller
        public ICollection<Auction> Auctions { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Rating> RatingsGiven { get; set; }
        public ICollection<Rating> RatingsReceived { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<ReportRequest> ReportRequests { get; set; }
    }
}
