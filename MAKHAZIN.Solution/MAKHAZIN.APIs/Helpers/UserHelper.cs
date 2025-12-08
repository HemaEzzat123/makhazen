using System.Security.Claims;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core;

namespace MAKHAZIN.Services.Helpers
{
    public interface IUserHelper
    {
        Task<int> GetUserIdAsync(ClaimsPrincipal user);
    }

    public class UserHelper : IUserHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetUserIdAsync(ClaimsPrincipal user)
        {
            var externalId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(externalId))
                return 0;

            var dbUser = await _unitOfWork.Repository<User>()
                .FindFirstOrDefaultAsync(u => u.ExternalId == externalId);

            return dbUser?.Id ?? 0;
        }
    }
}
