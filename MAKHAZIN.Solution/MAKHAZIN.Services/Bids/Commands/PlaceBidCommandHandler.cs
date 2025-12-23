using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Bids.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Core.Sepecification;
using MAKHAZIN.Core.Services.Contract;

namespace MAKHAZIN.Services.Bids.Commands
{
    public class PlaceBidCommandHandler : ICommandHandler<PlaceBidCommand, BidDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public PlaceBidCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }
        public async Task<Result<BidDTO>> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
        {

            var specification = new AuctionWithBidsSpecification(request.AuctionId);

            var auction = await _unitOfWork.Repository<Auction>().GetByIdWithSpecAsync(specification);

            if (auction == null)
                return Result<BidDTO>.Failure("Auction not found");

            // Prevent auction owner from bidding on their own auction
            if (auction.UserId == request.UserId)
                return Result<BidDTO>.Failure("You cannot bid on your own auction");

            if (auction.ExpirationTime < DateTime.UtcNow)
                return Result<BidDTO>.Failure("Auction has expired");

            var highestBid = auction.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault();
            var minimumBidPrice = highestBid != null ? highestBid.BidPrice : auction.StartingPrice;
            if (request.BidPrice <= minimumBidPrice)
                return Result<BidDTO>.Failure($"Bid price must be higher than {minimumBidPrice}");

            var bid = new Bid
            {
                AuctionId = request.AuctionId,
                UserId = request.UserId,
                BidPrice = request.BidPrice,
                Timestamp = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Bid>().AddAsync(bid);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(bid.UserId);
            var bidDto = new BidDTO
            {
                Id = bid.Id,
                AuctionId = bid.AuctionId,
                BidderName = user?.Name ?? "UnKnown",
                BidPrice = bid.BidPrice,
                TimeStamp = bid.Timestamp
            };

            // Notify the auction creator about the new bid
            await _notificationService.SendNotificationToUserAsync(auction.UserId, NotificationsMessages.NewBidTitle,
                string.Format(NotificationsMessages.NewBidMessage, bidDto.BidderName, bidDto.BidPrice));

            return Result<BidDTO>.Success(bidDto);
        }
    }
}
