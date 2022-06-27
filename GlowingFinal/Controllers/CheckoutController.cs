using GlowingFinal.DAL;
using GlowingFinal.Models;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }



    public static class Extension
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);


            //Extension.Put(TempData, "key1", test);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);


            //Extension.Get<VmSessionObject>(TempData, "key1");

        }
    }



    public class CheckoutController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public CheckoutController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        //public IActionResult Index()
        //{
        //    List<Country> countries = _context.Countries.ToList();
        //    countries.Insert(0, new Country { Id = 0, CountryName = "Country*" });
        //    ViewBag.Country = countries;

        //    string cart = Request.Cookies["cart"];
        //    List<VmCheckoutProduct> allInfoToProducts = new List<VmCheckoutProduct>();
        //    allInfoToProducts.DefaultIfEmpty();

        //    decimal subtotal = 0;
        //    Tax tax = _context.Tax.FirstOrDefault();

        //    if (cart != null)
        //    {
        //        List<string> prdList = cart.Split("/").ToList();

        //        foreach (var item in prdList)
        //        {
        //            VmCheckoutProduct prd = new VmCheckoutProduct();
        //            int id = Convert.ToInt32(item.Split("-")[0]);
        //            int quantity = Convert.ToInt32(item.Split("-")[1]);
        //            //AllInfoToProduct prdType = _context.AllInfoToProducts.Include(p => p.Product).ThenInclude(a => a.AllInfoToProducts).FirstOrDefault(t => t.Id == id);
        //            Product product = prdType.Product;

        //            if (prdType == null)
        //            {
        //                return RedirectToAction("errorpage", "home");
        //            }

        //            //type id
        //            prd.TypeId = prdType.Id;

        //            //product name
        //            prd.Name = prdType.Product.Name;

        //            //price or discount price
        //            if (prdType.DiscountDeadline > DateTime.Now && prdType.DiscountPrice > 0)
        //            {
        //                prd.Price = prdType.DiscountPrice;
        //            }
        //            else
        //            {
        //                prd.Price = prdType.Price;
        //            }
        //            //add cart product quantity
        //            prd.Quantity = quantity;

        //            allInfoToProducts.Add(prd);
        //        }

        //        //Calculate Subtotal
        //        foreach (var item in allInfoToProducts)
        //        {
        //            subtotal += item.Price * item.Quantity;
        //        }

        //    }

        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        //Find User
        //        string userId = _userManager.GetUserId(User);
        //        User user = _context.Users.Find(userId);

        //        Country userCountry = _context.Countries.Find(user.CountryId);
        //        decimal shippingPrice = 0;

        //        if (userCountry.ShippingPrice != null)
        //        {
        //            shippingPrice = (decimal)userCountry.ShippingPrice;
        //        }

        //        VmCheckout model1 = new VmCheckout()
        //        {
        //            //Layout
        //            Setting = _context.Settings.FirstOrDefault(),
        //            Socials = _context.SiteSocials.ToList(),
                   
        //            Payments = _context.Payments.ToList(),
        //            Contacts = _context.Contacts.ToList(),
        //            ContactInfo = _context.ContactInfos.FirstOrDefault(),
        //            Tax = _context.Tax.FirstOrDefault(),

        //            //SaleProduts
        //            SaleProduts = allInfoToProducts,

        //            //Subtotal
        //            SubTotal = subtotal,

        //            //Users infos return
        //            CountryId = user.CountryId,
        //            State = user.State,
        //            Zipcode = user.ZipCode,
        //            Phone = user.Phone,
        //            ShippingPrice = shippingPrice

        //        };

        //        return View(model1);
        //    }
        //    else
        //    {
        //        VmCheckout model = new VmCheckout()
        //        {
        //            //Layout
        //            Setting = _context.Settings.FirstOrDefault(),
        //            Socials = _context.SiteSocials.ToList(),
                  
        //            Payments = _context.Payments.ToList(),
        //            Contacts = _context.Contacts.ToList(),
        //            ContactInfo = _context.ContactInfos.FirstOrDefault(),

        //            //SaleProduts
        //            SaleProduts = allInfoToProducts,
        //            Tax = _context.Tax.FirstOrDefault(),

        //            //Subtotal
        //            SubTotal = subtotal

        //        };

        //        return View(model);
        //    }
        //}
    }
}
