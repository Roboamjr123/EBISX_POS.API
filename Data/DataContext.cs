using EBISX_POS.API.Models;
using EBISX_POS.API.Models.Manager;
using ManagerLibrary.Data;
using ManagerLibrary.ManagerData;
using Microsoft.EntityFrameworkCore;

namespace EBISX_POS.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Timestamp> Timestamp { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<DrinkType> DrinkType { get; set; }
        public DbSet<AddOnType> AddOnType { get; set; }
        public DbSet<StoreBranch> StoreBranch { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<CustomerReceipt> CustomerReceipt { get; set; }
        public DbSet<DailySalesSummary> DailySalesSummary { get; set; }
        public DbSet<OrderPurchaseSummary> OrderPurchaseSummary { get; set; }
        //public DbSet<ReportReceipt> ReportReceipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }



        // ✅ Auto-calculate subtotal before saving
        //public override int SaveChanges()
        //{
        //    foreach (var entry in ChangeTracker.Entries<Item>())
        //    {
        //        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.ItemSubTotal = (decimal)entry.Entity.ItemQTY * entry.Entity.ItemPrice;
        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}
