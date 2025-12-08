using System.Text.Json.Serialization;

namespace MAKHAZIN.Core.DTOs
{
    public class ProductDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string? ImageUrl { get; set; }
        public List<StockItemDTO> StockItems { get; set; } = new();
    }
}
