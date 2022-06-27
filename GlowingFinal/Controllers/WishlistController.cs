using GlowingFinal.DAL;
using GlowingFinal.Models;
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
    public class WishlistController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public WishlistController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            //if (User.Identity.IsAuthenticated)
            //{
            //    if (!User.IsInRole("User"))
            //    {
            //        return RedirectToAction("Logout", "Account");
            //    }
            //}

            RemoveArchived();

            if (User.Identity.IsAuthenticated)
            {
                RemoveArchivedUser();
                string oldData = _context.Users.Find(_userManager.GetUserId(User)).UserFavourite;

                if (!string.IsNullOrEmpty(oldData))
                {
                    var favourite = oldData.Split("-").ToList();


                    List<Product> _restourants = new List<Product>();
                    foreach (var f in favourite)
                    {
                        if (_context.Products.Find(Int32.Parse(f)) != null && _context.Products.Find(Int32.Parse(f)).Archived == false)
                        {
                            _restourants.Add(_context.Products.Include(p => p.ProductSizeToProducts).ThenInclude(ps => ps.ProductSize).FirstOrDefault(p => p.Id == Int32.Parse(f)));
                        }

                    }
                    VmWishlist model = new VmWishlist();
                    model.Setting = _context.Settings.FirstOrDefault();
                    model.SiteSocial = _context.SiteSocials.ToList();
                  
                    if (_restourants.Count > 0)
                    {
                        model.Products = _restourants;
                        return View(model);
                    }
                    return View(model);
                }
                else
                {
                    VmWishlist model = new VmWishlist();
                    model.Setting = _context.Settings.FirstOrDefault();
                    model.SiteSocial = _context.SiteSocials.ToList();
                    
                    return View(model);
                }

            }
            else
            {
                string oldData = Request.Cookies["favourites"];

                if (!string.IsNullOrEmpty(oldData))
                {
                    var favourite = oldData.Split("-").ToList();


                    List<Product> _restourants = new List<Product>();
                    foreach (var f in favourite)
                    {
                        if (_context.Products.Find(Int32.Parse(f)) != null && _context.Products.Find(Int32.Parse(f)).Archived == false)
                        {
                            _restourants.Add(_context.Products.Include(p => p.ProductImages).Include(p => 
                            p.ProductSizeToProducts).ThenInclude(ps => ps.ProductSize).FirstOrDefault(p => p.Id == Int32.Parse(f)));
                        }

                    }
                    VmWishlist model = new VmWishlist();
                    model.Setting = _context.Settings.FirstOrDefault();
                    model.SiteSocial = _context.SiteSocials.ToList();
               
                    if (_restourants.Count > 0)
                    {
                        model.Products = _restourants;
                        return View(model);
                    }
                    return View(model);
                }
                else
                {
                    VmWishlist model = new VmWishlist();
                    model.Setting = _context.Settings.FirstOrDefault();
                    model.SiteSocial = _context.SiteSocials.ToList();
                  

                    return View(model);
                }
            }


        }

        public void RemoveArchived()
        {
            string oldData = Request.Cookies["favourites"];
            string newData = null;

            if (!string.IsNullOrEmpty(oldData))
            {
                List<string> favouriteList = oldData.Split("-").ToList();

                foreach (var prd in favouriteList.ToList())
                {
                    if (_context.Products.Find(int.Parse(prd)) != null)
                    {
                        if (_context.Products.Find(int.Parse(prd)).Archived == true)
                        {
                            favouriteList.Remove(prd);
                            newData = string.Join("-", favouriteList);

                            CookieOptions options = new CookieOptions()
                            {
                                Expires = DateTime.Now.AddMonths(1)
                            };

                            Response.Cookies.Append("favourites", newData, options);

                        }
                    }
                }

            }

        }

        public void RemoveArchivedUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("User"))
                {
                    string oldData = _context.Users.Find(_userManager.GetUserId(User)).UserFavourite;
                    string newData = null;

                    if (!string.IsNullOrEmpty(oldData))
                    {
                        var favourite = oldData.Split("-").ToList();

                        foreach (var f in favourite.ToList())
                        {
                            if (_context.Products.Find(Int32.Parse(f)) != null && _context.Products.Find(Int32.Parse(f)).Archived == true)
                            {
                                favourite.Remove(f);
                                newData = string.Join("-", favourite);

                                _context.Users.Find(_userManager.GetUserId(User)).UserFavourite = newData;
                                _context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
    }
}
