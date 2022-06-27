using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Models
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public byte Quantity { get; set; }

        [ForeignKey("ProductSizeToProduct")]
        public int ProductSizeToProductId { get; set; }
        public ProductSizeToProduct ProductSizeToProduct { get; set; }

        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
