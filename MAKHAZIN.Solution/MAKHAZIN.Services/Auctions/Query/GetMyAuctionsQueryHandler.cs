using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auctions.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;

namespace MAKHAZIN.Services.Auctions.Query
{
    public class GetMyAuctionsQueryHandler : IQueryHandler<GetMyAuctionsQuery, Pagination<AuctionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMyAuctionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pagination<AuctionDTO>>> Handle(GetMyAuctionsQuery request, CancellationToken cancellationToken)
        {
            // Create specification with user filter and includes
            var spec = new AuctionsByUserSpecification(request.UserId, request.PageIndex, request.PageSize);

            // Create count specification
            var countSpec = new AuctionsByUserCountSpecification(request.UserId);

            // Get total count for pagination
            var totalItems = await _unitOfWork.Repository<Auction>().CountAsync(countSpec);

            // Get paginated auctions with navigation properties
            var auctions = await _unitOfWork.Repository<Auction>().GetAllWithSpecAsync(spec);

            // Map to DTOs
            var data = auctions.Select(a => new AuctionDTO
            {
                Id = a.Id,
                ProductName = a.Product?.Name ?? "Unknown Product",
                StartingPrice = a.StartingPrice,
                Quantity = a.Quantity,
                ExpirationTime = a.ExpirationTime,
                CreatedBy = a.User?.Name ?? "Unknown User",
                Bids = (a.Bids ?? new List<Bid>()).Select(b => new BidDTO
                {
                    Id = b.Id,
                    AuctionId = b.AuctionId,
                    BidPrice = b.BidPrice,
                    TimeStamp = b.Timestamp,
                    BidderName = b.User?.Name ?? "Unknown"
                }).ToList()
            }).ToList();

            var pagination = new Pagination<AuctionDTO>(request.PageIndex, request.PageSize, totalItems, data);

            return Result<Pagination<AuctionDTO>>.Success(pagination);
        }
    }
}
