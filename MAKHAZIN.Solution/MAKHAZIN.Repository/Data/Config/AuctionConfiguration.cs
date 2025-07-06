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
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.Property(a => a.StartingPrice).HasColumnType("decimal(18,2)");
            builder.Property(a => a.ExpirationTime).IsRequired();

            builder.HasMany(a => a.Bids)
                   .WithOne(b => b.Auction)
                   .HasForeignKey(b => b.AuctionId);
        }
    }

}
