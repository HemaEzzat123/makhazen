using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities.Identity
{
    public class RefreshToken
    {
        public int id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public DateTime ExpiresAtUTC { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
