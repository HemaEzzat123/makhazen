using System.Text.Json.Serialization;

namespace MAKHAZIN.Core.DTOs
{
    public class StockItemDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public float Discount { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
