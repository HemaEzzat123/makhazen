namespace MAKHAZIN.Core.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public float Discount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
