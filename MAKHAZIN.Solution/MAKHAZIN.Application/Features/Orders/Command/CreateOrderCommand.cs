using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Orders.Command
{
    public class CreateOrderCommand : ICommand<OrderDTO>
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
