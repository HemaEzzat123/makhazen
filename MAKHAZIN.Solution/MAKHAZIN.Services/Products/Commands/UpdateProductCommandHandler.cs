using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Products.Commands;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Products.Commands
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (product == null)
                return Result<bool>.Failure("Product not found");

            // Update product properties
            product.Name = request.Name;
            product.Description = request.Description;
            product.UnitOfMeasurement = request.UnitOfMeasurement;
            
            if (!string.IsNullOrEmpty(request.ImageUrl))
                product.ImageUrl = request.ImageUrl;

            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
