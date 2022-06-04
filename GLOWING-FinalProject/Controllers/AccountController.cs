using GLOWING_FinalProject.Constants;
using GLOWING_FinalProject.DAL;
using GLOWING_FinalProject.Models;
using GLOWING_FinalProject.Services;
using GLOWING_FinalProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLOWING_FinalProject.Controllers
{
    public class AccountController : Controller
    {


        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;

        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnviroment, IHttpContextAccessor httpContextAccessor, IMailService mailService)
        {
            _mailService = mailService;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _webHostEnviroment = webHostEnviroment;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<IActionResult> Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }










        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                ModelState.AddModelError("Username", "There is already user with this username");
                return View();
            }

            EndUser newUser = new EndUser
            {
                FullName = model.Fullname,
                UserName = model.Username,
                Email = model.Email,

            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            //await _userManager.AddToRoleAsync(newUser, RoleConstant.Users);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { newUser.UserName, token }, Request.Scheme);

            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = newUser.Email,
                Subject = "Complete registration",
                Body = link
            });

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest();

            var identityResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null) user = await _userManager.FindByEmailAsync(model.Login);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View();
            }

            //if (!user.IsActive)
            //{
            //    ModelState.AddModelError("", "Your account was blocked by admin");
            //    return View();
            //}

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }


    }
}
