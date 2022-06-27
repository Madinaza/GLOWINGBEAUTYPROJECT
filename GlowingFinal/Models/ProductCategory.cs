using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string CategoryName { get; set; }

        public List<ProductCategoryToProduct> ProductCategoryToProducts { get; set; }

    }
}
