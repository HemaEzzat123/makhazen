using MAKHAZIN.Core.Entities;
using MAKHAZIN.Repository.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository.Data
{
    public class MAKHAZINDbContext : DbContext
    {
        public MAKHAZINDbContext(DbContextOptions<MAKHAZINDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(MAKHAZINDbContext).Assembly); // Reflection to apply all configurations in the assembly
            modelBuilder.ApplyConfiguration(new UserAppConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StockItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new AuctionConfiguration());
            modelBuilder.ApplyConfiguration(new BidConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new ReportRequestConfiguration());
        }


        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ReportRequest> ReportRequests { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        #endregion
    }
}
