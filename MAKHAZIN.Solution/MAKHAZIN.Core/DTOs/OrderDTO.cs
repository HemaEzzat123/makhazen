namespace MAKHAZIN.Core.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        // Financial summary
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalAmount { get; set; }


        public List<OrderItemDTO> Items { get; set; }
    }
}
