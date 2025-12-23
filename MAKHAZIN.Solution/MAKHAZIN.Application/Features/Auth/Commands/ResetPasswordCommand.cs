using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class ResetPasswordCommand : ICommand<string>
    {
        public string Email { get; set; }
        public string Token { get; set; } // From the Link
        public string NewPassword { get; set; } 
    }
}
