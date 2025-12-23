using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System.Security.Claims;

namespace MAKHAZIN.Application.Features.Auth.Query
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
