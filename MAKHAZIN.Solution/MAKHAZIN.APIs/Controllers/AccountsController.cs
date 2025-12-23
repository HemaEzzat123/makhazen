using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MAKHAZIN.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, result.Error));
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, result.Error));

            return Ok(result);
        }
        [HttpPost("LoginWithRefreshToken")]
        public async Task<ActionResult<LoginResponseWithRefreshToken>> LoginWithRefreshToken(LoginCommandWithRefreshToken command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized, result.Error));
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, result.Error));
            return Ok(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, result.Error));
            return Ok(result);
        }
    }
}
