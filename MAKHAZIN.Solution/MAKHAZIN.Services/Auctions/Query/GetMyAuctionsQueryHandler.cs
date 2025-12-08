using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auctions.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
            var auctions = await _unitOfWork.Repository<Auction>().GetAllAsync();
            auctions = auctions.Where(a => a.UserId == request.UserId).ToList();

            // Get total count
            var totalItems = auctions.Count;

            // Apply pagination
            auctions = auctions
                .OrderByDescending(a => a.Id)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var data = auctions.Select(a => new AuctionDTO
            {
                Id = a.Id,
                ProductName = a.Product.Name,
                StartingPrice = a.StartingPrice,
                Quantity = a.Quantity,
                ExpirationTime = a.ExpirationTime,
                CreatedBy = a.User.Name,
                Bids = a.Bids.Select(b => new BidDTO
                {
                    Id = b.Id,
                    BidPrice = b.BidPrice,
                    TimeStamp = b.Timestamp
                }).ToList()
            }).ToList();

            var pagination = new Pagination<AuctionDTO>(request.PageIndex, request.PageSize, totalItems, data);

            return Result<Pagination<AuctionDTO>>.Success(pagination);
        }
    }
}
