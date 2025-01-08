using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerLaunch.Data;
using CareerLaunch.Models;

namespace CareerLaunch.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly AppDbContext _context;

        public BlogPostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BlogPosts
        public IActionResult Index(int page = 1, int pageSize = 9)
        {
            var blogPosts = _context.Blogs
                .OrderByDescending(b => b.PublishedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPosts = _context.Blogs.Count();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            return View(blogPosts);
        }

            // GET: BlogPosts/Details/5
            public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                // Log or throw an error, invalid ID
                return BadRequest("Invalid Blog Post ID.");
            }

            var blogPost = _context.Blogs.FirstOrDefault(b => b.BlogPostId == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }



        // GET: BlogPosts/Create
        public IActionResult Create()
        {
            var blogPost = new BlogPost();
            return View();
        }

        // POST: BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                if (blogPost.File != null && blogPost.File.Length > 0)
                {
                    try
                    {
                        
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                       
                        var fileName = Path.GetFileName(blogPost.File.FileName);

                        
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await blogPost.File.CopyToAsync(fileStream);
                        }

                       
                        blogPost.ImageUrl = $"/images/{fileName}";

                        
                        _context.Add(blogPost);
                        await _context.SaveChangesAsync();

                       
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                       
                        ModelState.AddModelError(string.Empty, "An error occurred while uploading the file.");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError("File", "Please upload a valid image file.");
                }
            }

           
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blogs.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogPost blogPost)
        {
            if (id != blogPost.BlogPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.BlogPostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.Blogs.FindAsync(id);
            if (blogPost != null)
            {
                _context.Blogs.Remove(blogPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogPostId == id);
        }
    }
}
