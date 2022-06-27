using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Moderator)]

    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SubscribeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Subscribe> s = await _context.Subscribes.OrderByDescending(e => e.Id).ToListAsync();
            return View(s);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.Subscribes.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var t = await _context.Subscribes.FindAsync(id);
            _context.Subscribes.Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
