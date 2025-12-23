using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Services.Contract;
using MAKHAZIN.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MAKHAZIN.Services.Auth.Commands
{
    public sealed class LoginCommandHandlerWithRefreshToken : ICommandHandler<LoginCommandWithRefreshToken, LoginResponseWithRefreshToken>
    {
        private readonly MAKHAZINIdentityDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public LoginCommandHandlerWithRefreshToken(MAKHAZINIdentityDbContext context, ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        public async Task<Result<LoginResponseWithRefreshToken>> Handle(LoginCommandWithRefreshToken request, CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _context.RefreshToken
                                                       .Include(r => r.User)
                                                       .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);
            if (refreshToken == null || refreshToken.ExpiresAtUTC < DateTime.UtcNow)
            {
                return Result<LoginResponseWithRefreshToken>.Failure("Invalid or expired refresh token.");
            }

            string accessToken = await _tokenService.CreateTokenAsync(refreshToken.User, _userManager);

            refreshToken.Token = _tokenService.GenerateRefreshToken();
            refreshToken.CreatedAtUTC = DateTime.UtcNow;
            refreshToken.ExpiresAtUTC = DateTime.UtcNow.AddDays(7);
            _context.RefreshToken.Update(refreshToken);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result<LoginResponseWithRefreshToken>.Failure($"Error saving refresh token: {ex.Message}");
            }
            return Result<LoginResponseWithRefreshToken>.Success(new LoginResponseWithRefreshToken
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token
            });
        }
    }
}
