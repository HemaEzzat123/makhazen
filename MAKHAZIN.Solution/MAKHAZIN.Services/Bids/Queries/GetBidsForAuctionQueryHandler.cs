using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Bids.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;

namespace MAKHAZIN.Services.Bids.Queries
{
    public class GetBidsForAuctionQueryHandler : IQueryHandler<GetBidsForAuctionQuery, List<BidDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBidsForAuctionQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BidDTO>>> Handle(GetBidsForAuctionQuery request, CancellationToken cancellationToken)
        {
            var speceification = new BidsForAuctionWithUserSpecification(request.AuctionId);
            var bids = await _unitOfWork.Repository<Bid>().GetAllWithSpecAsync(speceification);
            bids = bids.Where(b => b.AuctionId == request.AuctionId).ToList();

            var data = bids
                .OrderByDescending(b => b.BidPrice)
                .Select(b => new BidDTO
                {
                    Id = b.Id,
                    AuctionId = b.AuctionId,
                    BidPrice = b.BidPrice,
                    TimeStamp = b.Timestamp,
                    BidderName = b.User?.Name ?? "Unknown"
                }).ToList();

            return Result<List<BidDTO>>.Success(data);
        }
    }
}
