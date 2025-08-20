using MAKHAZIN.Core.Application.CQRS;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class LoginCommand : ICommand<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
