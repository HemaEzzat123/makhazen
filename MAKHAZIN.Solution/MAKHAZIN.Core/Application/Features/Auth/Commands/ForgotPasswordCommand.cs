using MAKHAZIN.Core.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class ForgotPasswordCommand : ICommand<string>
    {
        public string Email { get; set; }
    }
}
