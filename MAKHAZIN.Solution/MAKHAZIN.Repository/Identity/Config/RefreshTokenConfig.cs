using MAKHAZIN.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Identity.Config
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.id);
            builder.Property(r => r.Token)
                   .HasMaxLength(200);
            builder.HasIndex(r => r.Token)
                   .IsUnique();
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId);
        }
    }
}
