using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }

}
