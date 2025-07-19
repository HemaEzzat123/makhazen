using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Identity
{
    public class MAKHAZINIdentityDbContext : IdentityDbContext<AppUser>
    {
        public MAKHAZINIdentityDbContext(DbContextOptions<MAKHAZINIdentityDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(MAKHAZINIdentityDbContext).Assembly); // Reflection to apply all configurations in the assembly
        }
    }
}
