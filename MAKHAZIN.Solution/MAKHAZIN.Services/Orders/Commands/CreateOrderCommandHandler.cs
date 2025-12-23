using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Orders.Builder;
using MAKHAZIN.Application.Features.Orders.Command;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Orders.Commands
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderBuilder _orderBuilder;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderBuilder orderBuilder)
        {
            _unitOfWork = unitOfWork;
            _orderBuilder = orderBuilder;
        }
        public async Task<Result<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _unitOfWork.Repository<User>().GetByIdAsync(request.BuyerId);
            var seller = await _unitOfWork.Repository<User>().GetByIdAsync(request.SellerId);

            if (buyer is null || seller is null)
                throw new Exception("Buyer or Seller not found");

            
                _orderBuilder.setBuyer(request.BuyerId, buyer)
                .setSeller(request.SellerId, seller);

            foreach (var item in request.OrderItems)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                if (product is null)
                    throw new Exception($"Product with id {item.ProductId} not found");
                _orderBuilder.AddItem(product, item.Quantity, item.UnitPrice, item.Discount);
            }

            var order = _orderBuilder.Build();

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            // Calculate financial summary
            decimal totalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            decimal totalDiscount = order.OrderItems.Sum(oi => (decimal)oi.Discount * oi.Quantity);
            decimal finalAmount = totalAmount - totalDiscount;

            var dto = new OrderDTO
            {
                Id = order.Id,
                BuyerName = buyer?.Name ?? "Unknown",
                SellerName = seller?.Name ?? "Unknown",
                Status = order.Status.ToString(),
                OrderDate = order.OrderDate,
                TotalAmount = totalAmount,
                TotalDiscount = totalDiscount,
                FinalAmount = finalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    Discount = oi.Discount,
                    SubTotal = (oi.Price * oi.Quantity) - (decimal)oi.Discount * oi.Quantity
                }).ToList()
            };

            return Result<OrderDTO>.Success(dto);
        }
    }
}
