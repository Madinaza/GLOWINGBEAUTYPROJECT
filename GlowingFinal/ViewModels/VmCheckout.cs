using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmCheckout:VmBase
    {

        public VmPayment vmPayment { get; set; }
        public List<ProductSizeToProduct> prstp { get; set; }
        public List<string> Messages = new List<string>();
        public List<int> prqty = new List<int>();





    }
}
