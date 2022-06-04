using GLOWING_FinalProject.DAL;
using GLOWING_FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ProductVM model = new ProductVM {
                Products = await _context.Products.Take(16).ToListAsync()
            };

            return View(model);
        }
    }
}
