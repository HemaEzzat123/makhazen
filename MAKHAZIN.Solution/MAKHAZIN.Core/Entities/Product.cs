using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }

        // Navigation Properties
        public ICollection<StockItem> StockItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Auction> Auctions { get; set; }
    }

}
