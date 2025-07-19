using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Entities
{
    public class ReportRequest : BaseEntity
    {
        public int UserId { get; set; }

        public string ReportType { get; set; }     // e.g. Inventory, Sales
        public string FileFormat { get; set; }     // PDF, Excel
        public ReportStatus Status { get; set; }         // [Enum]
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
    }

}
