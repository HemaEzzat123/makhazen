using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Users.Command
{
    public class UpdateProfileCommand : ICommand<UserToReturnDTO>
    {
        public UpdateProfileCommand(string userId, string name, string phoneNumber, string address)
        {
            UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        
    }
}
