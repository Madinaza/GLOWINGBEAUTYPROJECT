using GlowingFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.ViewModels
{
    public class VmBase
    {
        public Setting Setting { get; set; }
        public List<SiteSocial> Socials { get; set; }
        public List<Contact> Contacts { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Payment> Payments { get; set; }

    }
}
