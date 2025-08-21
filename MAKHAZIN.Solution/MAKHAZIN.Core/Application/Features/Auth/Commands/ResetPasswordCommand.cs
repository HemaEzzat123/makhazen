using MAKHAZIN.Core.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class ResetPasswordCommand : ICommand<string>
    {
        public string Email { get; set; }
        public string Token { get; set; } // From the Link
        public string NewPassword { get; set; } 
    }
}
