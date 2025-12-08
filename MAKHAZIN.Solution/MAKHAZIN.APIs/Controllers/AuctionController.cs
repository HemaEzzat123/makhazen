using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core.Application.Features.Auctions.Commands;
using MAKHAZIN.Core.Application.Features.Auctions.Query;
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
    public class AuctionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserHelper _userHelper;

        public AuctionController(IMediator mediator, IUserHelper userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Get all auctions with pagination and optional filters
        /// </summary>
        /// <param name="search">Optional search term to filter auctions by product name</param>
        /// <param name="activeOnly">Filter to show only active auctions (not expired)</param>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of auctions</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<AuctionDTO>>> GetAllAuctions(
            [FromQuery] string? search,
            [FromQuery] bool? activeOnly,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var query = new GetAllAuctionsQuery
            {
                Search = search,
                ActiveOnly = activeOnly,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Get a specific auction by ID
        /// </summary>
        /// <param name="id">Auction ID</param>
        /// <returns>Auction details with bids</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuctionDTO>> GetAuctionById(int id)
        {
            var query = new GetAuctionByIdQuery { AuctionId = id };
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, result.Error));
        }

        /// <summary>
        /// Get active auctions only (not expired)
        /// </summary>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of active auctions</returns>
        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<AuctionDTO>>> GetActiveAuctions(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var query = new GetAllAuctionsQuery
            {
                ActiveOnly = true,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Get auctions created by the current user
        /// </summary>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of user's auctions</returns>
        [HttpGet("my-auctions")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult<Pagination<AuctionDTO>>> GetMyAuctions(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var userId = await _userHelper.GetUserIdAsync(User);

            var query = new GetMyAuctionsQuery
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
        /// Create a new auction (Seller only)
        /// </summary>
        /// <param name="command">Auction creation details</param>
        /// <returns>Created auction ID</returns>
        [HttpPost("create")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateAuction(CreateAuctionCommand command)
        {
            var userId = await _userHelper.GetUserIdAsync(User);
            command.UserId = userId;
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
                return Ok(new { auctionId = result.Value, message = "Auction created successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Update an existing auction (Seller only)
        /// </summary>
        /// <param name="id">Auction ID</param>
        /// <param name="command">Updated auction details</param>
        /// <returns>Success or failure</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> UpdateAuction(int id, UpdateAuctionCommand command)
        {
            if (id != command.AuctionId)
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Auction ID mismatch"));

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { message = "Auction updated successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Delete an auction (Seller or Admin only)
        /// </summary>
        /// <param name="id">Auction ID</param>
        /// <returns>Success or failure</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var command = new DeleteAuctionCommand { AuctionId = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { message = "Auction deleted successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
