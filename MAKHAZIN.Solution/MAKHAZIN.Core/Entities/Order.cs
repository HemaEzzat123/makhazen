using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Order : BaseEntity
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } // [Enum]

        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public User Buyer { get; set; }
        public User Seller { get; set; }
    }
}
