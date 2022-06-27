using FrontProject.Areas.Utils;
using GlowingFinal.Areas.Dashboard.ViewModels;
using GlowingFinal.Constants;
using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Moderator)]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProductController(AppDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var product = await _context.Products.Select(f => new ProductListViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                MainImage = f.MainImage
            }).ToListAsync();

            return View(product);
        }



        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.Include(f => f.ProductImages)
                   .Include(f => f.ProductComments)
                   .ThenInclude(c => c.User)
                   .Include(f => f.ProductCategoryToProduct)
                   .ThenInclude(fc => fc.ProductCategory).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }




        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Include(f => f.ProductImages)
                 .Include(f => f.ProductComments)
                 .ThenInclude(c => c.User)
                 .Include(f => f.ProductCategoryToProduct)
                 .ThenInclude(fc => fc.ProductCategory).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.Include(f => f.ProductImages)
                  .Include(f => f.ProductComments)
                  .ThenInclude(c => c.User)
                  .Include(f => f.ProductCategoryToProduct)
                  .ThenInclude(fc => fc.ProductCategory).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return NotFound();

            foreach (var image in product.ProductImages)
            {
                Fileutils.Delete(image.Image);
            }

            Fileutils.Delete(product.MainImage);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductPostViewModel model = new ProductPostViewModel
            {
                Campaigns = await _context.Campaigns.ToListAsync(),
                Categories = await _context.ProductCategories.ToListAsync()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductPostViewModel model)
        {
            model.Campaigns = await _context.Campaigns.ToListAsync();
            model.Categories = await _context.ProductCategories.ToListAsync();

            if (!ModelState.IsValid) return View(model);



       
            var campaign = await _context.Campaigns.FindAsync(model.CampaignId);
            if (campaign == null)
            {
                ModelState.AddModelError(nameof(ProductPostViewModel.CampaignId), "Choose a valid campaign");
                return View(model);
            }

          
            List<ProductCategoryToProduct> categories = new List<ProductCategoryToProduct>();
            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _context.ProductCategoryToProducts.FindAsync(categoryId);
                if (category == null)
                {
                    ModelState.AddModelError(nameof(ProductPostViewModel.CategoryIds), "Choose a valid category");
                    return View(model);
                }
                categories.Add(new ProductCategoryToProduct { ProductCategoryId = categoryId });
            }

           
            if (!model.MainImage.ContentType.Contains("image"))
            {
                ModelState.AddModelError(nameof(ProductPostViewModel.MainImage), "There is a problem in your file");
                return View(model);
            }

          
            List<ProductImage> images = new List<ProductImage>();
            foreach (var image in model.Images)
            {
                if (!image.ContentType.Contains("image"))
                {
                    ModelState.AddModelError(nameof(ProductPostViewModel.Images), $"There is a problem in your {image.FileName} file");
                    return View(model);
                }
                images.Add(new ProductImage
                {
                    Image = Fileutils.Create(Fileconstants.ImagePath, image)
                });
            }

            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
             
                DiscountPrice = model.Price - (model.Price * campaign.DiscountPercent / 100),
              
                SKU = model.SKUCode,
                ShortDesc = model.Desc,
                CampaignId = model.CampaignId,
                MainImage = Fileutils.Create(Fileconstants.ImagePath, model.MainImage),
                ProductImages = images,
                ProductCategoryToProduct = categories,
              
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.Include(f => f.ProductImages)
                  .Include(f => f.ProductComments)
                  .ThenInclude(c => c.User)
                  .Include(f => f.ProductCategoryToProduct)
                  .ThenInclude(fc => fc.ProductCategory).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return NotFound();

            ProductPostViewModel model = new ProductPostViewModel
            {
                CategoryIds = product.ProductCategoryToProduct.Select(c => c.ProductCategory.Id).ToList(),
                Name = product.Name,
                Desc = product.ShortDesc,
                Price = product.Price,
                SKUCode = product.SKU,
              
                CampaignId = product.CampaignId,
                Categories = await _context.ProductCategories.ToListAsync(),
                Campaigns = await _context.Campaigns.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductPostViewModel model)
        {
            var product = await _context.Products.Include(f => f.ProductImages)
                  .Include(f => f.ProductComments)
                  .ThenInclude(c => c.User)
                  .Include(f => f.ProductCategoryToProduct)
                  .ThenInclude(fc => fc.ProductCategory).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return NotFound();

            model.Categories = await _context.ProductCategories.ToListAsync();
            model.Campaigns = await _context.Campaigns.ToListAsync();

            if (!ModelState.IsValid) return View(model);

            //campaign
            var campaign = await _context.Campaigns.FindAsync(model.CampaignId);
            if (campaign == null)
            {
                ModelState.AddModelError(nameof(ProductPostViewModel.CampaignId), "Choose a valid campaign");
                return View(model);
            }

            //categories
            List<ProductCategoryToProduct> categories = new List<ProductCategoryToProduct>();
            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _context.ProductCategories.FindAsync(categoryId);
                if (category == null)
                {
                    ModelState.AddModelError(nameof(ProductPostViewModel.CategoryIds), "Choose a valid category");
                    return View(model);
                }
                categories.Add(new ProductCategoryToProduct { ProductCategoryId = categoryId });
            }

            if (model.MainImage != null)
            {
                if (!model.MainImage.ContentType.Contains("image"))
                {
                    ModelState.AddModelError(nameof(ProductPostViewModel.MainImage), "There is a problem in your file");
                    return View(model);
                }
                Fileutils.Delete(Path.Combine(Fileconstants.ImagePath, product.MainImage));
            }

            List<ProductImage> images = new List<ProductImage>();
            if (model.Images != null)
            {

                foreach (var image in model.Images)
                {
                    if (!image.ContentType.Contains("image"))
                    {
                        ModelState.AddModelError(nameof(ProductPostViewModel.Images), "There is a problem in your file");
                        return View(model);
                    }
                    images.Add(new ProductImage { ProductId = product.Id, Image = Fileutils.Create(Fileconstants.ImagePath, image) });
                };

                foreach (var image in product.ProductImages)
                {
                    Fileutils.Delete(image.Image);
                }
            }

            product.Name = model.Name;
            product.ShortDesc = model.Desc;

            product.SKU = model.SKUCode;
            product.Price = model.Price;
            product.DiscountPrice = model.Price - model.Price * campaign.DiscountPercent / 100;

            product.ProductCategoryToProduct = categories;
            product.ProductImages = images.Count > 0 ? images : product.ProductImages;
            product.MainImage = model.MainImage != null ? Fileutils.Create(Fileconstants.ImagePath, model.MainImage) : product.MainImage;


            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
