using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmFaq:VmBase
    {
        public Faq Faqs { get; set; }
        public List<Faq> faqs { get; set; }
    }
}
