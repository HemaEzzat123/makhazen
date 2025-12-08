using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Application.Features.Auctions.Query
{
    public class GetAuctionByIdQuery : IQuery<AuctionDTO>
    {
        public int AuctionId { get; set; }
    }
}
