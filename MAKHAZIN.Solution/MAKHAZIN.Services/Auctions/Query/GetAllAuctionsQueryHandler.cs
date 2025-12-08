using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auctions.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.Services.Auctions.Query
{
    public class GetAllAuctionsQueryHandler : IQueryHandler<GetAllAuctionsQuery, Pagination<AuctionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuctionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Pagination<AuctionDTO>>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
        {
            var auctions = await _unitOfWork.Repository<Auction>().GetAllAsync();

            // Filter by active status if specified
            if (request.ActiveOnly.HasValue && request.ActiveOnly.Value)
            {
                auctions = auctions.Where(a => a.ExpirationTime > DateTime.UtcNow).ToList();
            }

            // Search by product name
            if (!string.IsNullOrEmpty(request.Search))
            {
                auctions = auctions.Where(a => a.Product.Name.Contains(request.Search)).ToList();
            }

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
