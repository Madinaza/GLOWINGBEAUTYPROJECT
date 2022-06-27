using GlowingFinal.DAL;
using GlowingFinal.Models;
using GlowingFinal.Services;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    //[Route("[controller][action]")]
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public ShopController(AppDbContext context, UserManager<User> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _context = context;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("User"))
                {
                    return RedirectToAction("Logout", "Account");
                }
            }

            ProductVM model = new ProductVM {
                Products = await _context.Products.Include(c=>c.Campaign).Take(12).OrderByDescending(s=>s.Id).ToListAsync()
            };

            return View(model);
        }






        public async Task<IActionResult> Detail(int id)
        {
            Product product = _context.Products.Include(f => f.ProductImages).Include(c=>c.Campaign)

                .Include(f => f.ProductComments)
                .ThenInclude(c => c.User)
                .Include(f => f.ProductCategoryToProduct)
                .ThenInclude(fc => fc.ProductCategory).Include(i => i.Ingredient).Include(ps => ps.ProductSizeToProducts)
                .ThenInclude(s => s.ProductSize).
                FirstOrDefault(f => f.Id == id);

            if (product == null) return NotFound();



            ProductDetailVm model = new ProductDetailVm
            {
                Product = product,
                Products = await _context.Products.Take(4).ToListAsync()

            };

            return View(model);


        }
















        public IActionResult AddToWishlist(string productId)
        {

            if (productId != null)
            {
                if (_context.Products.Find(Int32.Parse(productId)) != null)
                {
                    if (_context.Products.Any(p => p.Id == Int16.Parse(productId)))
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            User endUser = _context.Users.Find(_userManager.GetUserId(User));

                            string oldData = endUser.UserFavourite;
                            string newData = null;

                            VmResponse response = new VmResponse();

                            if (string.IsNullOrEmpty(oldData))
                            {
                                newData = productId;
                                response.Success = "Added";
                            }
                            else
                            {
                                List<string> favouriteList = oldData.Split("-").ToList();
                                if (favouriteList.Any(f => f == productId))
                                {
                                    favouriteList.Remove(productId);
                                    newData = string.Join("-", favouriteList);
                                    response.Changed = "Removed";
                                }
                                else
                                {
                                    newData = oldData + "-" + productId;
                                    response.Success = "Added";
                                }
                            }

                            endUser.UserFavourite = newData;
                            _context.Users.Update(endUser);
                            _context.SaveChanges();
                            return Json(response);
                        }
                        else
                        {
                            string oldData = Request.Cookies["favourites"];
                            string newData = null;
                            VmResponse response = new VmResponse();
                            if (string.IsNullOrEmpty(oldData))
                            {
                                newData = productId;
                                response.Success = "Added";
                            }
                            else
                            {
                                List<string> favouriteList = oldData.Split("-").ToList();
                                if (favouriteList.Any(f => f == productId))
                                {
                                    favouriteList.Remove(productId);
                                    newData = string.Join("-", favouriteList);
                                    response.Changed = "Removed";
                                }
                                else
                                {
                                    newData = oldData + "-" + productId;
                                    response.Success = "Added";
                                }
                            }

                            CookieOptions options = new CookieOptions()
                            {
                                Expires = DateTime.Now.AddMonths(1)
                            };

                            Response.Cookies.Append("favourites", newData, options);

                            return Json(response);
                        }
                    }
                    else
                    {
                        VmResponse response = new VmResponse();
                        response.Error = "Error";

                        return Json(response);
                    }
                }
                else
                { 
                    VmResponse response = new VmResponse();
                    response.Error = "Error";

                    return Json(response);
                }
            }
            else
            {
                VmResponse response = new VmResponse();
                response.Error = "Error";

                return Json(response);
            }


        }

        public IActionResult RemoveFromWishlist(int? Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string oldData = _context.Users.Find(_userManager.GetUserId(User)).UserFavourite;
                string newData = null;

                if (!string.IsNullOrEmpty(oldData))
                {
                    List<string> favouriteList = oldData.Split("-").ToList();
                    if (favouriteList.Any(f => f == Id.ToString()))
                    {
                        favouriteList.Remove(Id.ToString());
                        newData = string.Join("-", favouriteList);
                        _context.Users.Find(_userManager.GetUserId(User)).UserFavourite = newData;
                        _context.SaveChanges();
                        return RedirectToAction("index", "Wishlist");
                    }
                    else
                    {
                        return RedirectToAction("index");
                    }
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            else
            {
                string oldData = Request.Cookies["favourites"];
                string newData = null;

                if (!string.IsNullOrEmpty(oldData))
                {
                    List<string> favouriteList = oldData.Split("-").ToList();
                    if (favouriteList.Any(f => f == Id.ToString()))
                    {
                        favouriteList.Remove(Id.ToString());
                        newData = string.Join("-", favouriteList);
                    }
                    else
                    {
                        return RedirectToAction("index");
                    }
                }
                else
                {
                    return RedirectToAction("index");
                }

                CookieOptions options = new CookieOptions()
                {
                    Expires = DateTime.Now.AddMonths(1)
                };

                Response.Cookies.Append("favourites", newData, options);

                return RedirectToAction("index", "Wishlist");
            }
        }






    }
}
