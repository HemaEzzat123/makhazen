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
    public class UpdateAuctionCommandHandler : ICommandHandler<UpdateAuctionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuctionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auctionRepo = _unitOfWork.Repository<Auction>();
            var auction = await auctionRepo.GetByIdAsync(request.AuctionId);

            if (auction is null)
                return Result<bool>.Failure(CommonResponses.AuctionNotFound);

            // Update only if values provided
            if (request.StartingPrice is not null)
                auction.StartingPrice = request.StartingPrice.Value;

            if (request.ExpirationTime is not null)
                auction.ExpirationTime = request.ExpirationTime.Value;

            if (request.Quantity is not null)
                auction.ProductId = request.Quantity.Value;

            auctionRepo.Update(auction);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
