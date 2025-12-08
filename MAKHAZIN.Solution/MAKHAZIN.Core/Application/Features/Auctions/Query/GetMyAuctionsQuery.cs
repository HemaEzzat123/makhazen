using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Application.Features.Auctions.Query
{
    public class GetMyAuctionsQuery : IQuery<Pagination<AuctionDTO>>
    {
        public int UserId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
