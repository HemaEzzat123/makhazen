﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
    }

}
