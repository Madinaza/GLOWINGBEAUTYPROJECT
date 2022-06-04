using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(35)]
        public string Size { get; set; }

        public List<ProductSizeToProduct> ProductSizeToProducts { get; set; }
    }
}
