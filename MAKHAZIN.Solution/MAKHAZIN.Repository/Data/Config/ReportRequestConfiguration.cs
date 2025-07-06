using MAKHAZIN.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAKHAZIN.Core.Enums;

namespace MAKHAZIN.Repository.Data.Config
{
    public class ReportRequestConfiguration : IEntityTypeConfiguration<ReportRequest>
    {
        public void Configure(EntityTypeBuilder<ReportRequest> builder)
        {
            builder.Property(r => r.ReportType).IsRequired().HasMaxLength(100);
            builder.Property(r => r.FileFormat).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Status)
                   .HasConversion(
                                    v => v.ToString(),                                      // to DB
                                    v => (ReportStatus)Enum.Parse(typeof(ReportStatus), v)) // from DB
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }

}
