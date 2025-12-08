using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Orders.Command;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;

namespace MAKHAZIN.Services.Orders.Commands
{
    public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
                return Result<bool>.Failure(CommonResponses.OrderNotFound);

            _unitOfWork.Repository<Order>().Delete(order);
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);
        }
    }
}
