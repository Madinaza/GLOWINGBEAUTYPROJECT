using GlowingFinal.DAL;
using GlowingFinal.Models;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmAbout model = new VmAbout()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),
              
                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),

                About = _context.Abouts.FirstOrDefault(),

                Partners = _context.Partners.ToList()
            };

            return View(model);
        }



        public IActionResult Subscribe(string email)
        {
            VmResponse response = new VmResponse();

            if (_context.Subscribes.FirstOrDefault(e => e.Email == email.ToLower()) == null)
            {
                Subscribe subscribe = new Subscribe();
                subscribe.Email = email.ToLower();
                subscribe.CreatedDate = DateTime.Now;
                _context.Subscribes.Add(subscribe);
                _context.SaveChanges();
                response.Success = "Thank you for subscribing";
                return Json(response);
            }
            else
            {
                response.Changed = "You already subscribed to our newsletter.";
                return Json(response);
            }

        }
    }
}
