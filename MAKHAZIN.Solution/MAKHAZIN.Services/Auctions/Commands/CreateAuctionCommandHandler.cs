using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auctions.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auctions.Commands
{
    public class CreateAuctionCommandHandler : ICommandHandler<CreateAuctionCommand, AuctionDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuctionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuctionDTO>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = new Auction
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                StartingPrice = request.StartingPrice,
                Quantity = request.Quantity,
                ExpirationTime = request.ExpirationTime,
                Bids = new List<Bid>()
            };

            await _unitOfWork.Repository<Auction>().AddAsync(auction);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(auction.UserId);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(auction.ProductId);

            var auctionDto = new AuctionDTO
            {
                Id = auction.Id,
                ProductName = product?.Name ?? "UnKnown",
                StartingPrice = auction.StartingPrice,
                Quantity = auction.Quantity,
                ExpirationTime = auction.ExpirationTime,
                CreatedBy = user?.Name ?? "UnKnown",
                Bids = new List<BidDTO>()
            };
            return Result<AuctionDTO>.Success(auctionDto);
        }
    }
}
