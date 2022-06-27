
using GlowingFinal.DAL;
using GlowingFinal.Models;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM
            {
                News = await _context.News.OrderByDescending(n => n.Id).Take(3).ToListAsync(),

                Sliders = await _context.Sliders.OrderBy(s => s.Order).ToListAsync(),
                Testimonials = await _context.Testimonials.OrderByDescending(n => n.Id).Take(3).ToListAsync(),
                Products = await _context.Products.Take(6).Include(c=>c.Campaign).ToListAsync()
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            if (ModelState.IsValid)
            {
                _context.Subscribes.Add(model);
                _context.SaveChanges();
                return Json(new
                {
                    error = false,
                    message = "Your request has been successfully registered.Please check your E-mail"
                });
            }

            return Json(new
            {
                error = true,
                message="Try again please"
            }) ;
        }


    }
}
