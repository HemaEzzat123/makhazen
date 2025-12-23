using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Application.Features.Users.Query
{
    public class GetUserProfileQuery : IQuery<UserProfileDTO>
    {
        public string UserId { get; set; }
        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
