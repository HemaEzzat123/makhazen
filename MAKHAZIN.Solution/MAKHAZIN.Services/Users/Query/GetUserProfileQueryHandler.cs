using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.User.Query;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace MAKHAZIN.Services.Users.Query
{
    public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, UserProfileDTO>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<UserProfileDTO>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if(user is null)
                return Result<UserProfileDTO>.Failure("User not found");

            var userProfileDto = new UserProfileDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user?.Email,
                Address = user?.Address ?? string.Empty
            };
            return Result<UserProfileDTO>.Success(userProfileDto);
        }
    }
}
