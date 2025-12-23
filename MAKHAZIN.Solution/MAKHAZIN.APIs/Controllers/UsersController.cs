using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Application.Features.Users.Command;
using MAKHAZIN.Application.Features.Users.Query;
using MAKHAZIN.Core.DTOs;
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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(userId))
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "User ID is not found in the claims."));

            var result = await _mediator.Send(new GetUserProfileQuery(userId));

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));

        }
        [HttpPatch("updateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(userId))
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "User ID is not found in the claims."));

            var result = await _mediator.Send(new UpdateProfileCommand(userId, request.Name, request.PhoneNumber, request.Address));

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
