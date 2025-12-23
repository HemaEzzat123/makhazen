using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Orders.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Orders.Queries
{
    public class GetMyOrdersQueryHandler : IQueryHandler<GetMyOrdersQuery, Pagination<OrderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pagination<OrderDTO>>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            // Filter by buyer or seller
            if (request.AsBuyer)
            {
                orders = orders.Where(o => o.BuyerId == request.UserId).ToList();
            }
            else
            {
                orders = orders.Where(o => o.SellerId == request.UserId).ToList();
            }

            // Get total count
            var totalItems = orders.Count;

            // Apply pagination
            orders = orders
                .OrderByDescending(o => o.Id)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var data = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                BuyerName = o.Buyer?.Name ?? "Unknown",
                SellerName = o.Seller?.Name ?? "Unknown",
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                Items = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    Discount = oi.Discount,
                    SubTotal = oi.Price * oi.Quantity * (1 - (decimal)oi.Discount)
                }).ToList(),
                TotalAmount = o.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                TotalDiscount = o.OrderItems.Sum(oi => oi.Price * oi.Quantity * (decimal)oi.Discount),
                FinalAmount = o.OrderItems.Sum(oi => oi.Price * oi.Quantity * (1 - (decimal)oi.Discount))
            }).ToList();

            var pagination = new Pagination<OrderDTO>(request.PageIndex, request.PageSize, totalItems, data);

            return Result<Pagination<OrderDTO>>.Success(pagination);
        }
    }
}
