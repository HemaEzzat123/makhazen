using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Bids.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;

namespace MAKHAZIN.Services.Bids.Queries
{
    public class GetHighestBidQueryHandler : IQueryHandler<GetHighestBidQuery, BidDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHighestBidQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BidDTO>> Handle(GetHighestBidQuery request, CancellationToken cancellationToken)
        {
            // Use specification to include User navigation property
            var specification = new BidsForAuctionWithUserSpecification(request.AuctionId);
            var bids = await _unitOfWork.Repository<Bid>().GetAllWithSpecAsync(specification);
            
            var highestBid = bids
                .OrderByDescending(b => b.BidPrice)
                .FirstOrDefault();

            if (highestBid == null)
                return Result<BidDTO>.Failure("No bids found for this auction");

            var bidDto = new BidDTO
            {
                Id = highestBid.Id,
                AuctionId = highestBid.AuctionId,
                BidPrice = highestBid.BidPrice,
                TimeStamp = highestBid.Timestamp,
                BidderName = highestBid.User?.Name ?? "Unknown"
            };

            return Result<BidDTO>.Success(bidDto);
        }
    }
}
