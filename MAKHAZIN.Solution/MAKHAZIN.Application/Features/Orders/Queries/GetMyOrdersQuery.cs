using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Orders.Queries
{
    public class GetMyOrdersQuery : IQuery<Pagination<OrderDTO>>
    {
        public int UserId { get; set; }
        public bool AsBuyer { get; set; } = true; // true for buyer orders, false for seller orders
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
