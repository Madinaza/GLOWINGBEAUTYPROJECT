using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmSessionObject
    {
        public List<int> ProductId = new List<int>();
        public List<int> ProductSizeToProductId = new List<int>();
        public List<int> ProductCount = new List<int>();
        public List<string> Messages = new List<string>();
    }
}
