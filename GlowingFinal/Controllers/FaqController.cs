using GlowingFinal.DAL;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public class FaqController : Controller
    {
        private readonly AppDbContext _context;

        public FaqController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmFaq model = new VmFaq()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),
              
                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),

                faqs = _context.Faq.ToList(),
            };

            return View(model);
        }
    }
}
