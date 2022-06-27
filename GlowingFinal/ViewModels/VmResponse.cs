using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmResponse
    {
        public string Success { get; set; }
        public string Error { get; set; }
        public string Changed { get; set; }
        public int CartCount { get; set; }
        public int StarsCount { get; set; }
        public List<Product> Products { get; set; }

        public bool QuantityError { get; set; }
    }
}
