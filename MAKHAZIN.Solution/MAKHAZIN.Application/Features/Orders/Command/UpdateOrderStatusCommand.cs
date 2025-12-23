using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Orders.Command
{
    public class UpdateOrderStatusCommand : ICommand<bool>
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}
