using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auctions.Commands;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auctions.Commands
{
    public class DeleteAuctionCommandHandler : ICommandHandler<DeleteAuctionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuctionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.Repository<Auction>().GetByIdAsync(request.AuctionId);
            if (auction == null)
                return Result<bool>.Failure(CommonResponses.AuctionNotFound);

            _unitOfWork.Repository<Auction>().Delete(auction);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
