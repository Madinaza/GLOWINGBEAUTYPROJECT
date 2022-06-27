using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class ProductVM:VmLay
    {
        
        public List<ProductSizeToProduct> productSizeToProducts { get; set; }
    
        public List<ProductCategory> ProductCats { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public Product SingleProduct { get; set; }
      
        public List<string> Favourite { get; set; }
      
    }
}
