using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auth.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auth.Query
{
    public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public GetCurrentUserQueryHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDTO>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var email = request.UserClaims.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Result<UserDTO>.Failure("Unauthorized Access");

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Result<UserDTO>.Failure("User not found");

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
