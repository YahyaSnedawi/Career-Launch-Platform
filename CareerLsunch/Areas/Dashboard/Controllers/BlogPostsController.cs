using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerLaunch.Data;
using CareerLaunch.Models;

namespace CareerLaunch.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BlogPostsController : Controller
    {
        private readonly AppDbContext _context;

        public BlogPostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/BlogPosts
        public async Task<IActionResult> Index()
        {
            var blogPosts = await _context.Blogs.ToListAsync();


            ViewBag.PendingBlogs = blogPosts.Where(o => o.Status == BlogPostStatus.Pending).ToList();
            ViewBag.AcceptedBlogs = blogPosts.Where(o => o.Status == BlogPostStatus.Accepted).ToList();
            ViewBag.RejectedBlogs = blogPosts.Where(o => o.Status == BlogPostStatus.Rejected).ToList();

            return View();
        }

        public async Task<IActionResult> Pend(int id)
        {
            var blogPost = await _context.Blogs.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Status = BlogPostStatus.Pending;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Accept(int id)
        {
            var blogPost = await _context.Blogs.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Status = BlogPostStatus.Accepted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id)
        {
            var blogPost = await _context.Blogs.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Status = BlogPostStatus.Rejected;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int id)
        {
            var blogPost = _context.Blogs.Find(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }
    }
}