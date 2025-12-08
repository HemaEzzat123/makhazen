namespace MAKHAZIN.Core.Entities
{
    public class Warehouse : BaseEntity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }

        public User User { get; set; }
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}
