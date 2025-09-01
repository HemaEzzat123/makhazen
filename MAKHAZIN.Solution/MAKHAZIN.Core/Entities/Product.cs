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
