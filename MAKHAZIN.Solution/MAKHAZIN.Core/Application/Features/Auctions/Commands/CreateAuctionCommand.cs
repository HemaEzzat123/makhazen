using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAKHAZIN.Core.Application.Features.Auctions.Commands
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
