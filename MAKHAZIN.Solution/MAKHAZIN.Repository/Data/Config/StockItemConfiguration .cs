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
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.Property(s => s.Quantity).IsRequired();
            builder.Property(s => s.SellingPrice).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Discount).HasDefaultValue(0);

            // Warehouse and Pharmacy relationships
            builder.HasOne(s => s.Warehouse)
                   .WithMany(w => w.StockItems)
                   .HasForeignKey(s => s.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Pharmacy)
                   .WithMany(p => p.StockItems)
                   .HasForeignKey(s => s.PharmacyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
