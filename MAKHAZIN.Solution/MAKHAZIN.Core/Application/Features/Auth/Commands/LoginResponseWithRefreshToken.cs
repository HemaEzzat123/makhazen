using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class LoginResponseWithRefreshToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
