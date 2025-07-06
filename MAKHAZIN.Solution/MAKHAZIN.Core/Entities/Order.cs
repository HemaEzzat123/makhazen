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
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } // [Enum]

        // Navigation
        public User Buyer { get; set; }
        public User Seller { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
