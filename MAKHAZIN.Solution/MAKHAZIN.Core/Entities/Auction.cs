using MAKHAZIN.Core.Entities.Identity;
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
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public decimal StartingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationTime { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }

}
