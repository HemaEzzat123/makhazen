using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Orders.Command
{
    public class DeleteOrderCommand : ICommand<bool>
    {
        public int OrderId { get; set; }
    }
}
