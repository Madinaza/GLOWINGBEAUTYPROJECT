using GlowingFinal.Areas.Dashboard.ViewModels;
using GlowingFinal.Constants;
using GlowingFinal.DAL;
using GlowingFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    //[Authorize(Roles = RoleConstant.Admin + "," + RoleConstant.Moderator)]
    public class UserController : Controller
    {

        private readonly AppDbContext _context;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null) return NotFound();
            List<UserVM> userList = new List<UserVM>();

            foreach (var user in users)
            {
                userList.Add(new UserVM
                {
                    Id = user.Id,
                    Fullname = user.Name+user.Surname,
                    Username = user.UserName,
                    Roles = string.Join(", ", (await _userManager.GetRolesAsync(user))),
                    Email = user.Email,
                    IsActive = user.IsActive
                });
            }

            return View(userList);
    }


        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.Name = user.Name;
            ViewBag.UserId = user.Id;
            return View(roles);
        }
        public async Task<IActionResult> RemoveRole(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction(nameof(ManageRoles), new { user.Id });
        }

        public async Task<IActionResult> AddRole(string id)
        {
            var roles = await _context.Roles.Select(r => r.Name).ToListAsync();

            AddRoleVM model = new AddRoleVM
            {
                UserId = id,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, AddRoleVM model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            await _userManager.AddToRoleAsync(user, model.RoleName);

            return RedirectToAction(nameof(ManageRoles), new { id });

        }


        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ViewBag.FullName = user.FullName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, ChangePassVM model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!ModelState.IsValid) return View();

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                ModelState.AddModelError(nameof(ChangePassVM.OldPassword), "Old Password is not correct");
                return View();
            }

            var idResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!idResult.Succeeded)
            {
                foreach (var error in idResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(Index));

        }



        public async Task<IActionResult> ToggleBlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
