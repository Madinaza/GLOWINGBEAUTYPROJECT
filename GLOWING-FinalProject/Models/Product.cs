using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class Product
    {
       [Key]
      public int Id { get; set; }
       [MaxLength(70), Required]
        public string Name { get; set; }
        [MaxLength(500), Required]
        public string ShortDesc { get; set; }
        public int Price { get; set; }

        [MaxLength(20)]
        public string SKU { get; set; }
        public ProductDetail Detail { get; set; }
        public ProductIngredient Ingredient { get; set; }

        public string HowToUse { get; set; }
        public string MainImage { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Archived { get; set; }

      

        [NotMapped]
        public List<int> ProductTagToProductId { get; set; }

        [NotMapped]
        public List<int> ProductCategoryToProducts { get; set; }

        public List<ProductTagToProduct> ProductTagToProducts { get; set; }
        public List<ProductCategoryToProduct> ProductCategoryToProduct { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductSizeToProduct> ProductSizeToProducts { get; set; }


    }
}
