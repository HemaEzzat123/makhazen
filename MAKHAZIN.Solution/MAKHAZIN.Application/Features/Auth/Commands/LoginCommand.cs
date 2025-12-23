using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class LoginCommand : ICommand<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
