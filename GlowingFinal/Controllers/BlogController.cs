using GlowingFinal.DAL;
using GlowingFinal.Models;
using GlowingFinal.Services;
using GlowingFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlowingFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public BlogController(AppDbContext context, UserManager<User> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _context = context;
            _mailService = mailService;
        }
        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.Take(12).Include(b => b.BlogCategoryToBlogs).ThenInclude(bc => bc.Category).ToListAsync();

            return View(blog);

        }

        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _context.Blogs.Include(b => b.BlogCategoryToBlogs).
                ThenInclude(bc => bc.Category).Include(c => c.Comments).ThenInclude(u=>u.User).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return NotFound();
            BlogVM model = new BlogVM
            {
                Blog = blog,

            };
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
     

        public async Task<IActionResult> AddComment(int id, BlogVM model)
        {
            var blog = await _context.Blogs.Include(b => b.BlogCategoryToBlogs).
      ThenInclude(bc => bc.Category).Include(c=>c.Comments).ThenInclude(u => u.User).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Blog = blog;
                return View(model);
            }

            var comment = new Comment
            {
                Description = model.Comment.Desc,
                UserId = _userManager.GetUserId(User),
                BlogId = id
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            //await _mailService.SendEmailAsync(new MailRequest
            //{
            //    ToEmail = blog.User.Email,
            //    Subject = "New Comment",
            //    Body = $"new comment to your {blog.Title} blog: {comment.Description}"
            //});

            return RedirectToAction(nameof(Detail), new { id });
        }

    }
}
