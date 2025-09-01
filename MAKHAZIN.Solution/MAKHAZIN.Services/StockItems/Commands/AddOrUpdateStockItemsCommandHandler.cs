using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.StockItems.Commands;
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
            var stockItem = await repo.FindFirstOrDefaultAsync(si => si.ProductId == request.ProductId && si.UserId == request.UserId);

            if (stockItem == null)
            {
                stockItem = new StockItem
                {
                    ProductId = request.ProductId,
                    UserId = request.UserId,
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
                await _unitOfWork.CompleteAsync();
            }
            await _unitOfWork.CompleteAsync();
            return Result<int>.Success(stockItem.Id);
        }
    }
}
