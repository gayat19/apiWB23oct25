using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Contexts
{
    public class ShoppingContext28Oct25 : DbContext
    {
        public ShoppingContext28Oct25(DbContextOptions<ShoppingContext28Oct25> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SpLoginReturn> SpLoginReturn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SpLoginReturn>().HasNoKey();

            modelBuilder.Entity<Order>(o =>
                {
                    o.Property(o => o.Total)
                    .HasColumnType("decimal(18,2)");

                });

            modelBuilder.Entity<Customer>().HasKey(c=>c.Id).HasName("PK_Customers_ID");
            modelBuilder.Entity<Order>()
                .HasKey(o=>o.OrderNumber)
                .HasName("PK_Orders_OrderNumber");

            modelBuilder.Entity<Order>()
                .HasOne(o=>o.Customer)
                .WithMany(c=>c.Orders)
                .HasForeignKey(o=>o.CustomerId)
                .HasConstraintName("FK_Orders_Customers_CustomerId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od=>od.SNo)
                .HasName("PK_OrderDetails_SNo");

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od=>od.Product)
                .WithMany(p=>p.OrderDetails)
                .HasForeignKey(od=>od.ProductId)
                .HasConstraintName("FK_OrderDetails_Products_ProductId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od=>od.Order)
                .WithMany(o=>o.OrderDetails)
                .HasForeignKey(od=>od.OrderNumber)
                .HasConstraintName("FK_OrderDetails_Orders_OrderNumber")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasKey(u=>u.Username).HasName("PK_Users_Username");

            modelBuilder.Entity<User>()
                .HasOne(u=>u.Customer)
                .WithOne(c=>c.User)
                .HasForeignKey<User>(User=>User.CustomerId)
                .HasConstraintName("FK_Users_Customers_CustomerId")
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
