using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class LoginCommand : ICommand<UserDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
