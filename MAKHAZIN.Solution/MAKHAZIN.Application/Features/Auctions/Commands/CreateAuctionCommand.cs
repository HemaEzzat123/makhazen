using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Auctions.Commands
{
    public class CreateAuctionCommand : ICommand<AuctionDTO>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal StartingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
