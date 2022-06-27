
using GlowingFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlowingFinal.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }


        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryToProduct> ProductCategoryToProducts { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductIngredient> ProductIngredients { get; set; }
       
       
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductTagToProduct> ProductTagToProducts { get; set; }
     
        public DbSet<Country> Countries { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryToBlog> BlogCategoryToBlogs { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SiteSocial> SiteSocials { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<BankCarts> BankCarts { get; set; }
        public DbSet<UnregisteredCustomer> unregisteredCustomers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        public DbSet<InvoiceNo> invoiceNos { get; set; }







        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductSizeToProduct> ProductSizeToProducts { get; set; }
        public DbSet<Partner> Partners { get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Message> Mesages { get; set; }
        public DbSet<Tax> Tax { get; set; }
      











































    }
}
