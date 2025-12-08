using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Users.Command
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
