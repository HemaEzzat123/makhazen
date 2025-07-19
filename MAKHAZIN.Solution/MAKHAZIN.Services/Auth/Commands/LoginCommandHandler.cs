using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auth.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
                return Result<UserDTO>.Failure("Invalid email or Password.... you forget something or you are imposter??!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);

            if(!result.Succeeded)
                return Result<UserDTO>.Failure("Invalid email or Password.... you forget something ??!");

            var token = await _tokenService.CreateTokenAsync(user, _userManager);
            var userDto = new UserDTO
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = token
            };
            return Result<UserDTO>.Success(userDto);
        }
    }
}
