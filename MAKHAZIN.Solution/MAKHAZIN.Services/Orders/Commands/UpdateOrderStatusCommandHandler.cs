using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Orders.Command;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;

namespace MAKHAZIN.Services.Orders.Commands
{
    public class UpdateOrderStatusCommandHandler : ICommandHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
                return Result<bool>.Failure(CommonResponses.OrderNotFound);

            if (!Enum.TryParse<OrderStatus>(request.NewStatus,true, out var newStatus))
                return Result<bool>.Failure(CommonResponses.InvalidOrderStatus);
            order.Status = newStatus;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);

        }
    }
}
