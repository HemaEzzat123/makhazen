using MAKHAZIN.Application.Features.Orders.Builder;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Orders.Builder
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order;
        public OrderBuilder()
        {
            _order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };
        }
        public IOrderBuilder setBuyer(int buyerId, User buyer)
        {
            _order.BuyerId = buyerId;
            _order.Buyer = buyer;
            return this;
        }

        public IOrderBuilder setSeller(int sellerId, User seller)
        {
            _order.SellerId = sellerId;
            _order.Seller = seller;
            return this;
        }

        public IOrderBuilder setStatus(OrderStatus orderStatus)
        {
            _order.Status = orderStatus;
            return this;
        }
        public IOrderBuilder AddItem(Product product, int quantity, decimal price, float discount)
        {
            var item = new OrderItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };
            _order.OrderItems.Add(item);
            return this;
        }
        public Order Build()
        {
            return _order;
        }
    }
}
