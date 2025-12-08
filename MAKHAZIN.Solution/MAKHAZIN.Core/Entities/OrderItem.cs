namespace MAKHAZIN.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }

        // Navigation
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
