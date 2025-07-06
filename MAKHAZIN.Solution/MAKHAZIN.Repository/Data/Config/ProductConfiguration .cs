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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.UnitOfMeasurement).IsRequired();

            builder.HasMany(p => p.StockItems)
                   .WithOne(s => s.Product)
                   .HasForeignKey(s => s.ProductId);

            builder.HasMany(p => p.OrderItems)
                   .WithOne(o => o.Product)
                   .HasForeignKey(o => o.ProductId);

            builder.HasMany(p => p.Auctions)
                   .WithOne(a => a.Product)
                   .HasForeignKey(a => a.ProductId);
        }
    }

}
