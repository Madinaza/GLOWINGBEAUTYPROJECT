using FrontProject.Areas.Utils;
using GLOWING_FinalProject.Constants;
using GLOWING_FinalProject.DAL;
using GLOWING_FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public NewsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var news = await _context.News.ToListAsync();
            return View(news);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Create(News homeNews)
        {
            if (ModelState.IsValid)
            {
                if (homeNews.ImageFile != null)
                {
                    if (homeNews.ImageFile.ContentType == "image/jpeg" || homeNews.ImageFile.ContentType == "image/png")
                    {
                        if (homeNews.ImageFile.Length < 3000000)
                        {
                            string ImageName2 = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMMMyyyy") + "-" + homeNews.ImageFile.FileName;
                            string FilePath2 = Path.Combine(_env.WebRootPath, "assets", "images", ImageName2);

                            using (FileStream Stream = new FileStream(FilePath2, FileMode.Create))
                            {
                                homeNews.ImageFile.CopyTo(Stream);
                            }

                            homeNews.Image = ImageName2;

                            _context.News.Add(homeNews);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            TempData["HomeSliderError2"] = "The size of the Image file must be less than 3 MB";
                            return View(homeNews);
                        }
                    }
                    else
                    {
                        TempData["HomeSliderError2"] = "The type of Image file can only be jpeg/jpg or png";
                        return View(homeNews);
                    }
                }
                else
                {
                    TempData["HomeSliderError2"] = "Image field must not be empty. Please choose a image";
                    return View(homeNews);
                }
            }

            return View(homeNews);
        }



        public async Task<IActionResult> Delete(int id)
        {
            News news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteNews(int id)
        {

            News news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            Fileutils.Delete(Path.Combine(Fileconstants.ImagePath, news.Image));
            _context.News.Remove(news);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int id)
        {
            News news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }


        public async Task<IActionResult> Update(int id)
        {
            News news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(News model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                    {
                        if (model.ImageFile.Length < 3000000)
                        {


                            if (!string.IsNullOrEmpty(model.Image))
                            {
                                string oldImagePath = Path.Combine(_env.WebRootPath, "assets", "images", model.Image);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }


                            string ImageName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMMMyyyy") + "-" + model.ImageFile.FileName;
                            string FilePath = Path.Combine(_env.WebRootPath, "assets", "images", ImageName);

                            using (var Stream = new FileStream(FilePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(Stream);
                            }

                            model.Image = ImageName;

                        }
                        else
                        {
                            TempData["HomeSliderError3"] = "The size of the Image file must be less than 3 MB";
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["HomeSliderError3"] = "The type of Image file can only be jpeg/jpg or png";
                        return View(model);
                    }

                }

                _context.News.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }





        }
    }
}
