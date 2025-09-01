using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Products.Commands;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Products.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                UnitOfMeasurement = request.UnitOfMeasurement
            };

            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return Result<int>.Success(product.Id);
        }
    }
}
