using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ContactController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public JsonResult addSubscribe(string email)
        {
            if (email == null)
            {
                return Json(404);
            }

            bool isExist = _context.Subscribes.Any(e => e.Email == email);

            if (isExist)
            {
                return Json(505);
            }

            Subscribe subscribe = new Subscribe()
            {
                Email = email,
                CreatedDate = DateTime.Now
            };

            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();

            return Json(200);
        }
    }
}

