using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Application.Features.Orders.Command
{
    public class CreateOrderCommand : ICommand<OrderDTO>
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
