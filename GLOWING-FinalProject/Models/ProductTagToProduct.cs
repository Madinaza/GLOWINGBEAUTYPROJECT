using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class ProductTagToProduct
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductTag")]
        public int ProductTagId { get; set; }
        public ProductTag ProductTag { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
