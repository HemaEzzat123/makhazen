using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Products.Commands;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Products.Commands
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (product == null)
                return Result<bool>.Failure("Product not found");

            // Check if product has associated stock items
            var hasStockItems = product.StockItems?.Any() ?? false;
            if (hasStockItems)
                return Result<bool>.Failure("Cannot delete product with existing stock items. Please remove stock items first.");

            // Check if product has associated auctions
            var hasAuctions = product.Auctions?.Any() ?? false;
            if (hasAuctions)
                return Result<bool>.Failure("Cannot delete product with existing auctions. Please remove auctions first.");

            _unitOfWork.Repository<Product>().Delete(product);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
