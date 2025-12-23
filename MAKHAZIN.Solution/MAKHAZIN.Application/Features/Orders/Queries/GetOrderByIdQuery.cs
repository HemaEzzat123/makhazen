using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Orders.Queries
{
    public class GetOrderByIdQuery : IQuery<OrderDTO>
    {
        public int OrderId { get; set; }
        public GetOrderByIdQuery(int orderId) => OrderId = orderId;
    }
}
