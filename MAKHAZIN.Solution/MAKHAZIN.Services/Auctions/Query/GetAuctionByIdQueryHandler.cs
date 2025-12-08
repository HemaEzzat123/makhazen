using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auctions.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Core.Sepecification;

namespace MAKHAZIN.Services.Auctions.Query
{
    public class GetAuctionByIdQueryHandler : IQueryHandler<GetAuctionByIdQuery, AuctionDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuctionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuctionDTO>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
        {

            var spec = new AuctionWithBidsSpecification(request.AuctionId);
            var auction = await _unitOfWork.Repository<Auction>().GetByIdWithSpecAsync(spec);

            if (auction == null)
                return Result<AuctionDTO>.Failure(CommonResponses.AuctionNotFound);

            var auctionDto = new AuctionDTO
            {
                Id = auction.Id,
                CreatedBy = auction.User != null ? auction.User.Name : "UnKnown",
                ExpirationTime = auction.ExpirationTime,
                ProductName = auction.Product.Name,
                Quantity = auction.Quantity,
                StartingPrice = auction.StartingPrice,
                Bids = auction.Bids.Select(b => new BidDTO
                {
                    Id = b.Id,
                    AuctionId = b.AuctionId,
                    BidderName = b.User != null ? b.User.Name : "UnKnown",
                    BidPrice = b.BidPrice,
                    TimeStamp = b.Timestamp
                }).ToList(),
            };
            return Result<AuctionDTO>.Success(auctionDto);
        }
    }
}
