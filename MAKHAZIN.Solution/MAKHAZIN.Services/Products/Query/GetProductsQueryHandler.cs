using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Products.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;

namespace MAKHAZIN.Services.Products.Query
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Pagination<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pagination<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithFiltirationSpecification(

                                                                request.Search,
                                                                (request.PageIndex - 1) * request.PageSize,
                                                                request.PageSize);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var data = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                StockItems = p.StockItems.Select(si => new StockItemDTO
                {
                    Id = si.Id,
                    Quantity = si.Quantity,
                    SellingPrice = si.SellingPrice,
                    Discount = si.Discount,
                    ExpirationDate = si.ExpirationDate
                }).ToList(),

            }).ToList();

            var countSpec = new ProductsWithFiltirationSpecification(request.Search, 0, int.MaxValue);

            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

            var pagination = new Pagination<ProductDTO>(request.PageIndex, request.PageSize, totalItems, data);

            return Result<Pagination<ProductDTO>>.Success(pagination);
        }
    }
}
