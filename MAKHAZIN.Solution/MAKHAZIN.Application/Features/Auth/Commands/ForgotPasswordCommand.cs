using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : ICommand<string>
    {
        public string Email { get; set; }
    }
}
