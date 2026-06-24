using Microsoft.EntityFrameworkCore;
using Newsstand_World.Model;

namespace Newsstand_World.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Purchaser> Purchasers { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
    }
}
