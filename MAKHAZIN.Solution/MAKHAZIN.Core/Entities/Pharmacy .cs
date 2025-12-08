namespace MAKHAZIN.Core.Entities
{
    public class Pharmacy : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public string LicenseNumber { get; set; }
        public string Address { get; set; }

        public User User { get; set; }
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}
