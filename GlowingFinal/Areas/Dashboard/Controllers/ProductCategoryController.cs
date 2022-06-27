using GlowingFinal.Constants;
using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = RoleConstant.Admin)]
    public class ProductCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public ProductCategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductCategory> categories = await _context.ProductCategories.ToListAsync();
            return View(categories);
        }

  

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.ProductCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ProductCategory category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductCategory category)
        {
            bool isExist = await _context.ProductCategories.AnyAsync(c => c.Id == id);
            if (!isExist) return NotFound();

            if (category == null) return NotFound();
            if (id != category.Id) return BadRequest();
            if (!ModelState.IsValid) return View();

            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            ProductCategory category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            return PartialView(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            ProductCategory category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
