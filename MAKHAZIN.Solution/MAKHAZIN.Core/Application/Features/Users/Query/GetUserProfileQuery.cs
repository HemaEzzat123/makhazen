using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Users.Query
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
