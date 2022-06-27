using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmAbout:VmBase
    {
        public About About { get; set; }
       
        public List<Partner> Partners { get; set; }
    }
}
