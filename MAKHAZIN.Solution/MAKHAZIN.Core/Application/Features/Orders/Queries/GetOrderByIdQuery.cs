using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Application.Features.Orders.Queries
{
    public class GetOrderByIdQuery : IQuery<OrderDTO>
    {
        public int OrderId { get; set; }
    }
}
