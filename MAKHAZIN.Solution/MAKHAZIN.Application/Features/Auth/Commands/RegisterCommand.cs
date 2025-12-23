using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Auth.Commands
{
    public class RegisterCommand : ICommand<UserDTO>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
