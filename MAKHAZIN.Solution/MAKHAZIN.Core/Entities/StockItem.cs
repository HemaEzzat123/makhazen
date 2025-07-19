using MAKHAZIN.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class StockItem : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public float Discount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Product Product { get; set; }

        // Navigation
        public User User { get; set; }
    }

}
