using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Auth.Query
{
    public class GetCurrentUserQuery : IQuery<UserDTO>
    {
        public ClaimsPrincipal UserClaims { get; set; }
        public GetCurrentUserQuery(ClaimsPrincipal userClaims)
        {
            UserClaims = userClaims ?? throw new ArgumentNullException(nameof(userClaims), "User claims cannot be null");
        }
    }
}
