using MAKHAZIN.Core.Application.CQRS;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class LoginCommandWithRefreshToken : ICommand<LoginResponseWithRefreshToken>
    {
        public string RefreshToken { get; set; }
    }
}
