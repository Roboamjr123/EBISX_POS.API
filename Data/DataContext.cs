using EBISX_POS.API.Models;
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

        public DbSet<StoreBranch> StoreBranches { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
               .HasOne(o => o.Receipt)        // Order has one Receipt
               .WithOne(cr => cr.Order)       // Receipt has one Order
               .HasForeignKey<Receipt>(cr => cr.OrderId); // Receipt.OrderId is the FK
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
