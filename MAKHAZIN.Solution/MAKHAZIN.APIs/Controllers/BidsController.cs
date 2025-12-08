using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Core.Application.Features.Bids.Commands;
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
        [HttpPost("placeBid")]
        public async Task<IActionResult> PlaceBid(PlaceBidCommand command)
        {
            var userId = await _userHelper.GetUserIdAsync(User);
            command.UserId = userId;
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
