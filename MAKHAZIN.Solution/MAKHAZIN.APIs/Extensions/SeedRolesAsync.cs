using MAKHAZIN.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace MAKHAZIN.APIs.Extensions
{
    public static class SeedRoles
    {
        public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { UserRoles.Admin, UserRoles.Buyer, UserRoles.Seller };

            foreach (var role in roles)
            {
                if (!await RoleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };
                    await RoleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
