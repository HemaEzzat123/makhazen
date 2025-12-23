using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Products.Query
{
    public class GetProductsQuery : IQuery<Pagination<ProductDTO>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
