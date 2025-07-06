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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.Status)
                   .HasConversion(
                                    v => v.ToString(),                                    // to DB
                                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v)) // from DB
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);
        }
    }

}
