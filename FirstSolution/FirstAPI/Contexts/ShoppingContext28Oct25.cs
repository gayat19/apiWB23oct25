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
    }
}
