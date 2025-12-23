using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core;
using MAKHAZIN.Application.Features.Orders.Command;
using MAKHAZIN.Application.Features.Orders.Queries;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Services.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MAKHAZIN.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserHelper _userHelper;

        public OrdersController(IMediator mediator, IUserHelper userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Get a specific order by ID
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, result.Error));
        }

        /// <summary>
        /// Get orders for the current user
        /// </summary>
        /// <param name="asBuyer">True to get orders as buyer, false to get orders as seller (default: true)</param>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of user's orders</returns>
        [HttpGet("my-orders")]
        public async Task<ActionResult<Pagination<OrderDTO>>> GetMyOrders(
            [FromQuery] bool asBuyer = true,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var userId = await _userHelper.GetUserIdAsync(User);

            var query = new GetMyOrdersQuery
            {
                UserId = userId,
                AsBuyer = asBuyer,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="command">Order creation details</param>
        /// <returns>Created order ID</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var buyerId = await _userHelper.GetUserIdAsync(User);
            if (buyerId == 0)
                return Unauthorized(new ApiResponse((int)HttpStatusCode.Unauthorized));
            
            command.BuyerId = buyerId;
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
                return Ok(new { orderId = result.Value, message = "Order created successfully" });
            
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Update order status (Seller or Admin only)
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="command">Status update details</param>
        /// <returns>Success or failure</returns>
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusCommand command)
        {
            if (id != command.OrderId)
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Order ID mismatch"));

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { message = "Order status updated successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
