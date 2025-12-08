using MAKHAZIN.Core.Application.CQRS;

namespace MAKHAZIN.Core.Application.Features.Orders.Command
{
    public class DeleteOrderCommand : ICommand<bool>
    {
        public int OrderId { get; set; }
    }
}
