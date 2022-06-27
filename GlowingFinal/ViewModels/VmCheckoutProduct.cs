﻿using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmCheckoutProduct:LayoutVM
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
