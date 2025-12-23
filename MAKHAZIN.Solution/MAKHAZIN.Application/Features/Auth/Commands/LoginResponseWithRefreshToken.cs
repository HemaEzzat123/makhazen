namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class LoginResponseWithRefreshToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
