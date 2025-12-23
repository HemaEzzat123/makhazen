using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Bids.Queries
{
    public class GetMyBidsQuery : IQuery<Pagination<BidDTO>>
    {
        public int UserId { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
