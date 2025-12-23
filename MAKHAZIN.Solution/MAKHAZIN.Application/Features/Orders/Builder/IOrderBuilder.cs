using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;

namespace MAKHAZIN.Application.Features.Orders.Builder
{
    public interface IOrderBuilder
    {
        IOrderBuilder setBuyer(int buyerId, User buyer);
        IOrderBuilder setSeller(int sellerId, User seller);
        IOrderBuilder setStatus(OrderStatus orderStatus);
        IOrderBuilder AddItem(Product product, int quantity, decimal price,float discount);
        Order Build();
    }
}
