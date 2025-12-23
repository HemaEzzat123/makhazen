using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Auctions.Query
{
    public class GetAuctionByIdQuery : IQuery<AuctionDTO>
    {
        public int AuctionId { get; set; }
    }
}
