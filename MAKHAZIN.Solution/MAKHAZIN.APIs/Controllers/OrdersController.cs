using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.Features.Orders.Command;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var buyerId = await _userHelper.GetUserIdAsync(User);
            if (buyerId == 0)
                return Unauthorized(new ApiResponse((int)HttpStatusCode.Unauthorized));
            command.BuyerId = buyerId;
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
