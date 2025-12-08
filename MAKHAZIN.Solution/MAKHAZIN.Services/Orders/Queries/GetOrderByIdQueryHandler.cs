using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Orders.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;

namespace MAKHAZIN.Services.Orders.Queries
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
                return Result<OrderDTO>.Failure(CommonResponses.OrderNotFound);

            var orderDto = new OrderDTO
            {
                Id = order.Id,
                BuyerName = order.Buyer?.Name ?? "Unknown",
                SellerName = order.Seller?.Name ?? "Unknown",
                Status = order.Status.ToString(),
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                TotalDiscount = order.OrderItems.Sum(oi => (decimal)oi.Discount * oi.Quantity),
                FinalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity) -
                              order.OrderItems.Sum(oi => (decimal)oi.Discount * oi.Quantity),
                Items = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    Discount = oi.Discount,
                    SubTotal = (oi.Price * oi.Quantity) - (decimal)oi.Discount * oi.Quantity
                }).ToList()
            };
            return Result<OrderDTO>.Success(orderDto);
        }
    }
}
