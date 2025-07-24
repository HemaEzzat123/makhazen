using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Repository.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Identity
{
    public static class MAKHAZINIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager,MAKHAZINDbContext context)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Name = "Ahmed Emad",
                    Email = "eng.ahmed.emad.work@gmail.com",
                    UserName = "ahmed.emad",
                    Address = "Cairo,Egypt",
                    
                    PhoneNumber = "01028618665"
                };
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, UserRoles.Admin);

                if (result.Succeeded)
                {
                    var domainUser = new User  // Here you would typically add the user to a database context
                    {
                        ExternalId = user.Id,
                        Name = user.Name,
                        Address = user.Address,
                        IsActive = true,
                    };        
                    await context.Users.AddAsync(domainUser);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
