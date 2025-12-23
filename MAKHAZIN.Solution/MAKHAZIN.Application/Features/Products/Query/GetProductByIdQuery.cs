using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;
namespace MAKHAZIN.Application.Features.Products.Query
{
    public class GetProductByIdQuery : IQuery<ProductDTO>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id) => Id = id;
    }
}
