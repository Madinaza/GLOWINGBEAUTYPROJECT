using GlowingFinal.Constants;
using GlowingFinal.DAL;
//using GlowingFinal.Migrations;
using GlowingFinal.Models;
using GlowingFinal.Services;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace GlowingFinal.Controllers
{
    public class AccountController : BaseController
    
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMailService _mailService;

        public AccountController(RoleManager<IdentityRole> roleManager,AppDbContext context,UserManager<User> userManager, IMailService mailService, SignInManager<User> signInManager ,IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _userManager = userManager;
            _mailService = mailService;
            _signinManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
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
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account was blocked by admin");
                return View();
            }

            var signinResult = await _signinManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                ModelState.AddModelError("Username", "This user is already exsist");
                return View();
            }

            User newUser = new User
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

            await _userManager.AddToRoleAsync(newUser, RoleConstant.User);


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


        






        public IActionResult ForgotPassword()
        {
            VmForgotPassword model = new VmForgotPassword()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),

                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),
            };

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(VmForgotPassword model)
        {
            //layout
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.SiteSocials.ToList();
            model.Payments = _context.Payments.ToList();
            model.Contacts = _context.Contacts.ToList();
            model.ContactInfo = _context.ContactInfos.FirstOrDefault();

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null /*&& await _userManager.IsEmailConfirmedAsync(user)*/)
                {
                    // Generate the reset password token
                    var token = HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));


                    //Sending mail
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("glowingbeautyreplyy@gmail.com", "Glowing");
                    mail.To.Add(model.Email);
                    mail.Body = "<h1>Hello!</h1> <br />" +
                        "<p>For resetting password please visit the link below</p>" +
                        "<a href='https://localhost:44305/account/resetpassword?email=" + model.Email + "&token=" + token + "'>Reset password</a>";

                    mail.IsBodyHtml = true;
                    mail.Subject = "Reset password";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("glowingbeautyreplyy@gmail.com", "wcqaegbzliylbgus");

                    smtpClient.Send(mail);
                    //End of sending mail


                    Notify("Please check your email account for reseting password.");
                }

                return RedirectToAction("login", "account");
            }

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {

            if (token == null || email == null)
            {
                Notify("Invalid password reset token!", notificationType: NotificationType.error);
                ModelState.AddModelError("", "Invalid password reset token");
            }

            VmResetPassword model = new VmResetPassword()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),
                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),

            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(VmResetPassword model)
        {
            //layout
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.SiteSocials.ToList();
            model.Payments = _context.Payments.ToList();
            model.Contacts = _context.Contacts.ToList();
            model.ContactInfo = _context.ContactInfos.FirstOrDefault();

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        Notify("Password changed successfully");
                        return RedirectToAction("login", "account");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }


                return RedirectToAction("login", "account");
            }

            return View(model);
        }





        [Authorize]
        public IActionResult Profile()
        {
            //Find User
            string userId = _userManager.GetUserId(User);
            User user = _context.Users.Find(userId);

            VmProfile model = new VmProfile()
            {
                //Layout
                Setting = _context.Settings?.FirstOrDefault(),
                Socials = _context.SiteSocials?.ToList(),
             
                Payments = _context.Payments?.ToList(),
                Contacts = _context.Contacts?.ToList(),
                ContactInfo = _context.ContactInfos?.FirstOrDefault(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                State = user.State,
                Zipcode = user.ZipCode,
                Image = user.Image,
             
            };

            return View(model);
        }


        [Authorize]
        public IActionResult UpdateProfile()
        {
           

            //Find User
            string userId = _userManager.GetUserId(User);
            User user = _context.Users.Find(userId);

            VmProfile model = new VmProfile()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),
              
                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                State = user.State,
                Zipcode = user.ZipCode,
                Image = user.Image,
              
            };

            return View(model);
        }



        [HttpPost]
        [Authorize]
        [Obsolete]
        public IActionResult UpdateProfile(VmProfile model)
        {
            //Find User
            string userId = _userManager.GetUserId(User);
            User user = _context.Users.Find(userId);

            //if (ModelState.IsValid)
            //{
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/webp" || model.ImageFile.ContentType == "image/gif")
                    {
                        if (model.ImageFile.Length <= 5242880)
                        {
                            if (model.Image != null)
                            {
                                //delete old picture if old picture is exist
                                string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "/Images", model.Image);
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                            }


                            //add new picture
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "/Images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            user.Name = model.Name;
                            user.Surname = model.Surname;
                            user.Email = model.Email;
                            user.Phone = model.Phone;
                            user.ZipCode = model.Zipcode;
                            user.State = model.State;
                          
                            user.Image = model.Image;

                            //update
                            _context.Entry(user).State = EntityState.Modified;

                            //if i dont want change something - title is a example property
                            //_context.Entry(model).Property(a => a.Title).IsModified = false;

                            _context.SaveChanges();
                            return RedirectToAction("profile", "account");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "The file image exceeds the maximum allowed size (5 MB).");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "You can only upload files in jpeg, png, webp, gif formats.");
                        return View(model);
                    }
            }

            user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.ZipCode = model.Zipcode;
                user.State = model.State;
             
                //update
                _context.Entry(user).State = EntityState.Modified;

                //if i dont want change something - title is a example property
                //_context.Entry(model).Property(a => a.Title).IsModified = false;

                _context.SaveChanges();
                return RedirectToAction("profile", "account");

        //};


            return View(model);
        }





        [Authorize]
        public IActionResult ChangePassword()
        {
            VmChangePassword model = new VmChangePassword()
            {
                //Layout
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.SiteSocials.ToList(),
                Payments = _context.Payments.ToList(),
                Contacts = _context.Contacts.ToList(),
                ContactInfo = _context.ContactInfos.FirstOrDefault(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(VmChangePassword model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "account");
                }

                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    Notify("The password has not been changed!", notificationType: NotificationType.error);
                    ModelState.AddModelError("", "New Password or Confirm Password is incorrect.");
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                    if (!result.Succeeded)
                    {
                        Notify("The password has not been changed!", notificationType: NotificationType.error);
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    await _signinManager.RefreshSignInAsync(user);
                    if (result.Succeeded)
                    {
                        Notify("Password Changed successfully");
                        return RedirectToAction("profile", "account");
                    }
                }

            }

            //layout
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.SiteSocials.ToList();
          
            model.Payments = _context.Payments.ToList();
            model.Contacts = _context.Contacts.ToList();
            model.ContactInfo = _context.ContactInfos.FirstOrDefault();

            return View(model);
        }



        //public async Task<IActionResult> RoleCreator()
        //{
        //     await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return Ok();
        //}


        //[Authorize]
        //public IActionResult MyOrders()
        //{
        //    //Find User
        //    string userId = _userManager.GetUserId(User);
        //    User user = _context.Users.Find(userId);

        //    List<VmOrder> vmOrders = new List<VmOrder>();
        //    List<Sale> sales = _context.Sales.Where(s => s.UserId == userId).ToList();
        //    List<SaleItem> saleItems = new List<SaleItem>();

        //    foreach (var sale in sales)
        //    {
        //        List<SaleItem> currentSaleItems = _context.SaleItems.Include(s => s.Sale).Include(p => p.AllInfoToProduct)
        //            .ThenInclude(a => a.Product).ThenInclude(i => i.ProductImages).Where(s => s.SaleId == sale.Id).
        //            Include(p => p.AllInfoToProduct)
        //            .ThenInclude(size => size.Size).ToList();

        //        foreach (var saleitem in currentSaleItems)
        //        {
        //            saleItems.Add(saleitem);
        //        }
        //    }




        //    VmOrder model = new VmOrder()
        //    {
        //        //Layout
        //        Setting = _context.Settings.FirstOrDefault(),
        //        Socials = _context.Socials.ToList(),
        //        Universals = _context.Universal.ToList(),
        //        Payments = _context.Payments.ToList(),
        //        Contacts = _context.Contacts.ToList(),
        //        ContactInfo = _context.ContactInfo.FirstOrDefault(),
        //        SaleItems = saleItems,
        //        Sales = sales
        //    };

        //    return View(model);
        //}











    }
}
