using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Data.Config
{
    internal class UserAppConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasMany(u => u.OrdersPlaced)
                   .WithOne(o => o.Buyer)
                   .HasForeignKey(o => o.BuyerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.OrdersReceived)
                   .WithOne(o => o.Seller)
                   .HasForeignKey(o => o.SellerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Auctions)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Bids)
                   .WithOne(b => b.User)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.RatingsGiven)
                   .WithOne(r => r.Rater)
                   .HasForeignKey(r => r.RaterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.RatingsReceived)
                   .WithOne(r => r.Ratee)
                   .HasForeignKey(r => r.RateeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.UserId);

            builder.HasMany(u => u.ReportRequests)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId);

            builder.HasIndex(u => u.ExternalId)
                   .IsUnique();
        }
    }
}
