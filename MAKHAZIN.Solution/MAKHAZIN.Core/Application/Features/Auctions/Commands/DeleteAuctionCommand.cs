using MAKHAZIN.Core.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Auctions.Commands
{
    public class DeleteAuctionCommand : ICommand<bool>
    {
        public int AuctionId { get; set; }
    }
}
