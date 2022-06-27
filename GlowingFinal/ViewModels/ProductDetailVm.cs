using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class ProductDetailVm
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
        public List<string> Favourite { get; set; }
    }
}
