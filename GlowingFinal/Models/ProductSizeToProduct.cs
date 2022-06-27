using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class ProductSizeToProduct
    {
        [Key]
        public int Id { get; set; }

        [Range(0.0, 100000000.0, ErrorMessage = "Price must be greter than zero!")]
        [Required]
        public decimal Price { get; set; }

        [Range(0, byte.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        [Required]
        public byte Quantity { get; set; }

        [ForeignKey("ProductSize")]
        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public List<SaleItem> SaleItems { get; set; }


    }
}
