using FrontProject.Areas.Extensions;
using FrontProject.Areas.Utils;
using GlowingFinal.Constants;
using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        //public IActionResult Create(Slider homeSlider)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (homeSlider.ImageFile != null)
        //        {
        //            if (homeSlider.ImageFile.ContentType == "image/jpeg" || homeSlider.ImageFile.ContentType == "image/png")
        //            {
        //                if (homeSlider.ImageFile.Length < 3000000)
        //                {
        //                    string ImageName2 = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMMMyyyy") + "-" + homeSlider.ImageFile.FileName;
        //                    string FilePath2 = Path.Combine(_env.WebRootPath, "assets", "images", ImageName2);

        //                    using (FileStream Stream = new FileStream(FilePath2, FileMode.Create))
        //                    {
        //                        homeSlider.ImageFile.CopyTo(Stream);
        //                    }

        //                    homeSlider.Image = ImageName2;

        //                    _context.Sliders.Add(homeSlider);
        //                    _context.SaveChanges();
        //                    return RedirectToAction("Index");

        //                }
        //                else
        //                {
        //                    TempData["HomeSliderError2"] = "The size of the Image file must be less than 3 MB";
        //                    return View(homeSlider);
        //                }
        //            }
        //            else
        //            {
        //                TempData["HomeSliderError2"] = "The type of Image file can only be jpeg/jpg or png";
        //                return View(homeSlider);
        //            }
        //        }
        //        else
        //        {
        //            TempData["HomeSliderError2"] = "Image field must not be empty. Please choose a image";
        //            return View(homeSlider);
        //        }
        //    }

        //    return View(homeSlider);
        //}



        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (!slider.ImageFile.IsSupported("image"))
            {
                ModelState.AddModelError(nameof(slider.ImageFile), "File type is unsupported");
                return View();
            }

            slider.Image = Fileutils.Create(Fileconstants.ImagePath, slider.ImageFile);


            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            Fileutils.Delete(Path.Combine(Fileconstants.ImagePath, slider.Image));
            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }


        [ValidateAntiForgeryToken]
         [HttpPost]
        public IActionResult Update(Slider model)
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

                _context.Sliders.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }





        }












        //public async Task<IActionResult> Update(int id, Slider slider)
        //{
        //    var dbSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        //    if (dbSlider == null) return NotFound();

        //    if (!ModelState.IsValid) return View(slider);
        //    dbSlider.Title = slider.Title;
        //    dbSlider.Description = slider.Description;
        //    dbSlider.SignatureTitle = slider.SignatureTitle;
        //    dbSlider.Image = Fileutils.Create(Fileconstants.ImagePath, slider.ImageFile);

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //}

        public async Task<IActionResult> Detail(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

    }
}
