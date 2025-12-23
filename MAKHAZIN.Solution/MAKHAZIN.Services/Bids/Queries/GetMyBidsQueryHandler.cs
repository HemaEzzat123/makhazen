using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Bids.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Services.Bids.Queries
{
    public class GetMyBidsQueryHandler : IQueryHandler<GetMyBidsQuery, Pagination<BidDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyBidsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pagination<BidDTO>>> Handle(GetMyBidsQuery request, CancellationToken cancellationToken)
        {
            var bids = await _unitOfWork.Repository<Bid>().GetAllAsync();
            bids = bids.Where(b => b.UserId == request.UserId).ToList();

            // Get total count
            var totalItems = bids.Count;

            // Apply pagination
            bids = bids
                .OrderByDescending(b => b.Timestamp)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var data = bids.Select(b => new BidDTO
            {
                Id = b.Id,
                AuctionId = b.AuctionId,
                BidPrice = b.BidPrice,
                TimeStamp = b.Timestamp,
                BidderName = b.User?.Name ?? "Unknown"
            }).ToList();

            var pagination = new Pagination<BidDTO>(request.PageIndex, request.PageSize, totalItems, data);

            return Result<Pagination<BidDTO>>.Success(pagination);
        }
    }
}
