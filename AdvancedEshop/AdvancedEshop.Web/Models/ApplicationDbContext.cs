using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using AdvancedEshop.Web.API.Models;
using AdvancedEshop.Web.Models;

namespace AdvancedEshop.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Order> Orders => Set<Order>();







    }

}
