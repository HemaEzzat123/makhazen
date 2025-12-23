using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class LoginCommandWithRefreshToken : ICommand<LoginResponseWithRefreshToken>
    {
        public string RefreshToken { get; set; }
    }
}
