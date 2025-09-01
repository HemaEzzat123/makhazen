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
    }
}
