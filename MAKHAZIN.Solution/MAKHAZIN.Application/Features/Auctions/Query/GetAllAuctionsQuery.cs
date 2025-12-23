using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Auctions.Query
{
    public class GetAllAuctionsQuery : IQuery<Pagination<AuctionDTO>>
    {
        public string? Search { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public bool? ActiveOnly { get; set; } = null;
    }
}
