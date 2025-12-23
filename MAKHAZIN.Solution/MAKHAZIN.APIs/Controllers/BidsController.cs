using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Application.Features.Bids.Commands;
using MAKHAZIN.Application.Features.Bids.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Services.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MAKHAZIN.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserHelper _userHelper;

        public BidsController(IMediator mediator, IUserHelper userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Get all bids for a specific auction (Sellers and Admins)
        /// </summary>
        /// <param name="auctionId">Auction ID</param>
        /// <returns>List of bids ordered by price (highest first)</returns>
        [HttpGet("auction/{auctionId}")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<ActionResult<List<BidDTO>>> GetBidsForAuction(int auctionId)
        {
            var query = new GetBidsForAuctionQuery(auctionId);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Get the highest bid for a specific auction (All authenticated users)
        /// </summary>
        /// <param name="auctionId">Auction ID</param>
        /// <returns>Highest bid details</returns>
        [HttpGet("auction/{auctionId}/highest")]
        public async Task<ActionResult<BidDTO>> GetHighestBid(int auctionId)
        {
            var query = new GetHighestBidQuery(auctionId);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, result.Error));
        }

        /// <summary>
        /// Get bids placed by the current user (Buyers only)
        /// </summary>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of user's bids</returns>
        [HttpGet("my-bids")]
        [Authorize(Roles = "Buyer,Seller")]
        public async Task<ActionResult<Pagination<BidDTO>>> GetMyBids(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var userId = await _userHelper.GetUserIdAsync(User);

            var query = new GetMyBidsQuery
            {
                UserId = userId,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Place a bid on an auction (Buyers only)
        /// </summary>
        /// <param name="command">Bid details</param>
        /// <returns>Created bid ID</returns>
        [HttpPost("placeBid")]
        [Authorize(Roles = "Buyer,Seller")]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidCommand command)
        {
            var userId = await _userHelper.GetUserIdAsync(User);
            command.UserId = userId;
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
                return Ok(new { bidId = result.Value, message = "Bid placed successfully" });
            
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
