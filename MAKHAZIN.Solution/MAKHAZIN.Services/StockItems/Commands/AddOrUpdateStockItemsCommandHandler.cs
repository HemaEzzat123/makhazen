using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.StockItems.Commands;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.StockItems.Commands
{
    public class AddOrUpdateStockItemsCommandHandler : ICommandHandler<AddOrUpdateStockItemsCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddOrUpdateStockItemsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(AddOrUpdateStockItemsCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<StockItem>();
            StockItem? stockItem = null;

            if (request.PharmacyId.HasValue)
            {
                stockItem = await repo.FindFirstOrDefaultAsync(
                    si => si.ProductId == request.ProductId && si.PharmacyId == request.PharmacyId
                );
            }
            else if (request.WarehouseId.HasValue)
            {
                stockItem = await repo.FindFirstOrDefaultAsync(
                    si => si.ProductId == request.ProductId && si.WarehouseId == request.WarehouseId
                );
            }
            if (stockItem == null)
            {
                stockItem = new StockItem
                {
                    ProductId = request.ProductId,
                    PharmacyId = request.PharmacyId,
                    WarehouseId = request.WarehouseId,
                    Quantity = request.Quantity,
                    SellingPrice = request.SellingPrice,
                    Discount = request.Discount,
                    ExpirationDate = request.ExpirationDate
                };
                await repo.AddAsync(stockItem);
            }
            else
            {
                stockItem.Quantity += request.Quantity;
                stockItem.SellingPrice = request.SellingPrice;
                stockItem.Discount = request.Discount;
                stockItem.ExpirationDate = request.ExpirationDate;
                repo.Update(stockItem);
            }
            await _unitOfWork.CompleteAsync();
            return Result<int>.Success(stockItem.Id);
        }
    }
}
