using MAKHAZIN.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Rating : BaseEntity
    {
        public int RaterId { get; set; }
        public int RateeId { get; set; }

        public int Score { get; set; }
        public string Comment { get; set; }

        // Navigation
        public User Rater { get; set; }
        public User Ratee { get; set; }
    }

}
