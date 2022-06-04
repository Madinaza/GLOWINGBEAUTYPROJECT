using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string DetailPart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
