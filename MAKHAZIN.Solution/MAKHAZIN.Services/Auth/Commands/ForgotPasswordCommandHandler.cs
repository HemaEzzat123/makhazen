using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Core.Services.Contract;
using MAKHAZIN.Services.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly FrontendSettings _options;

        public ForgotPasswordCommandHandler(UserManager<AppUser> userManager, IEmailService emailService, IOptions<FrontendSettings> options)
        {
            _userManager = userManager;
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return Result <string>.Failure(CommonResponses.UserNotFound);


            // Generate Reset Token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Encode Token to make it safe for URLs
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // Create Reset Link

            var resetLink = $"{_options.ClientURL}/reset-password?email={request.Email}&token={encodedToken}";

            // Send Email
            var subject = "Reset Your Password";
            var body = $"Hello {user.Name ?? user.Email},<br/><br/>" +
                       $"Please reset your password by clicking <a href='{resetLink}'>here</a>.<br/><br/>" +
                       $"If you did not request a password reset, please ignore this email.";

            _emailService.SendEmail(user.Email, subject, body);

            return Result<string>.Success("Password reset link has been sent to your email.");
        }
    }
}
