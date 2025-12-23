namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
