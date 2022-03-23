using Microsoft.EntityFrameworkCore;

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
       public DbSet<Dimension> Dimensions { get; set; }
       public DbSet<Media> Medias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Mobile>().ToTable("Mobile");
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
            modelBuilder.Entity<Dimension>().ToTable("Dimension");
            modelBuilder.Entity<Media>().ToTable("Media");


        }
    }
}
