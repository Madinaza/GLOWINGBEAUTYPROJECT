using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class ShopVM:VmBase
    {
        public List<Product> Products { get; set; }
        public List<Product> AllProducts { get; set; }
        public Product Product { get; set; }
       
  
        public List<ProductCategory> Categories { get; set; }
     
        //public List<AllInfoToProduct> AllInfoToProducts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductComment> Comments { get; set; }
        public ProductComment Comment { get; set; }
        public ProductDetail ProductDetail { get; set; }
        

        public int? catId { get; set; }
        
        public int? sizeId { get; set; }
      
        public int? SortId { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public ShopVM Filter { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        //public List<AllInfoToProduct> FeaturedAllInfoToProducts { get; set; }
        public List<ProductComment> FeaturedComments { get; set; }
        public List<ProductImage> FeaturedProductImages { get; set; }
    }
}
