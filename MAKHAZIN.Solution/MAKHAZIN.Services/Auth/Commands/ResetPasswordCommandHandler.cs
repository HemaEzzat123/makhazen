using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
                return Result<string>.Failure(CommonResponses.UserNotFound);

            // Decode the token from the URL
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            // Reset the password
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (result.Succeeded)
                return Result<string>.Success("Password has been reset successfully.");
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<string>.Failure($"Failed to reset password: {errors}");
            }
        }
    }
}
