using MAKHAZIN.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Data.Config
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Title).HasMaxLength(100).IsRequired();
            builder.Property(n => n.Message).HasMaxLength(500);
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }

}
