using Microsoft.EntityFrameworkCore;
using Electro_Project.Models;

namespace Electro_Project.Models.Context
{
    public class ShopContext: DbContext
    {

        public ShopContext():base()
        {

        }

        public ShopContext(DbContextOptions<ShopContext> options) :base(options)
        {

        }
       public DbSet<Product> Products { get; set; }
       public DbSet<Mobile> Mobiles { get; set; }
       public DbSet<Laptop> Laptops { get; set; }
       public DbSet<Media> Medias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Mobile>().ToTable("Mobile");
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
            modelBuilder.Entity<Media>().ToTable("Media");


        }


        public DbSet<Electro_Project.Models.Manufacturer> Manufacturer { get; set; }
    }
}
