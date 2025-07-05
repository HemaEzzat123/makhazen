using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository
{
    public class MAKHAZINDbContext: DbContext
    {
        public MAKHAZINDbContext(DbContextOptions<MAKHAZINDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Reflection to apply all configurations in the assembly
        }


        #region DbSets

        #endregion
    }
}
