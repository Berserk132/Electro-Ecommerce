using Microsoft.EntityFrameworkCore;
using Electro_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Electro_Project.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Electro_Project.Models.Context
{
    //public class ShopContext: DbContext
    public class ShopContext : IdentityDbContext<AppUser>
    {


        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<RoleViewModel> RoleViewModel { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<WishList_Product> WishLists_Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Mobile>().ToTable("Mobile");
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
            modelBuilder.Entity<Media>().ToTable("Media");


            modelBuilder.Entity<WishList_Product>()
                .HasKey(V => new { V.UserID, V.PID });

            base.OnModelCreating(modelBuilder); /// For IdentityDbContext Mapping
        }



    }
}
