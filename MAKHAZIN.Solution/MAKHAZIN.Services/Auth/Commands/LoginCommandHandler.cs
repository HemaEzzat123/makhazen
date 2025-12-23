using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Core.Services.Contract;
using MAKHAZIN.Repository.Identity;
using Microsoft.AspNetCore.Identity;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand,LoginResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly MAKHAZINIdentityDbContext _identityDbContext;

        public LoginCommandHandler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            MAKHAZINIdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _identityDbContext = identityDbContext;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
                return Result<LoginResponse>.Failure(CommonResponses.UserNotFound);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);

            if(!result.Succeeded)
                return Result<LoginResponse>.Failure(CommonResponses.InvalidCredentials);

            var token = await _tokenService.CreateTokenAsync(user, _userManager);
            var userDto = new UserDTO
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = token
            };
            // Generate a refresh token
            var refreshToken = new RefreshToken
            {
                Token = _tokenService.GenerateRefreshToken(),
                CreatedAtUTC = DateTime.UtcNow,
                ExpiresAtUTC = DateTime.UtcNow.AddDays(7),
                UserId = user.Id,
                User = user
            };
            // Save the refresh token to the database
            try
            {
                await _identityDbContext.RefreshToken.AddAsync(refreshToken);
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return Result<LoginResponse>.Success(new LoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                Email = user.Email,
                DisplayName = user.Name
            });
        }
    }
}
