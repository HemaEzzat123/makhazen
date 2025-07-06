using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Auction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public decimal StartingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationTime { get; set; }

        // Navigation
        public User User { get; set; }
        public Product Product { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }

}
