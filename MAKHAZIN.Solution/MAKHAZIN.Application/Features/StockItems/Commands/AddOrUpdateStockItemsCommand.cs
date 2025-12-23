using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.StockItems.Commands
{
    public class AddOrUpdateStockItemsCommand : ICommand<int>
    {
        public int? PharmacyId { get; set; }
        public int? WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public float Discount { get; set; }
        public DateTime? ExpirationDate { get; set; }


    }
}
