using GLOWING_FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GLOWING_FinalProject.DAL
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryToProduct> ProductCategoryToProducts { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductSizeToProduct> ProductSizeToProducts { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductTagToProduct> ProductTagToProducts { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
















    }
}
