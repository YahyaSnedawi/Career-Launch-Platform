using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerLaunch.Data;
using CareerLaunch.Models;
using Microsoft.AspNetCore.Identity;
using CareerLaunch.Models.ViewModel;

namespace CareerLaunch.Controllers
{
    public class JobPostsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public JobPostsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: JobPosts
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10; 

            
            var jobPosts = _context.JobPosts
                .OrderByDescending(j => j.PostDate) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

       
            var totalJobs = _context.JobPosts.Count();

            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalJobs / pageSize);

            return View(jobPosts); 
        }



        // GET: JobPosts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);

            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }


        // GET: JobPosts/Create
        public IActionResult Create()
        {
            var jobPost = new JobPost(); 
            return View(jobPost); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var employerId = _userManager.GetUserId(User);
                    jobPost.EmployerId = employerId;

                    if (jobPost.File != null && jobPost.File.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var fileName = Path.GetFileName(jobPost.File.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await jobPost.File.CopyToAsync(fileStream);
                        }

                        jobPost.UploadedFilePath = $"/images/{fileName}";
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Please upload a valid file.");
                        return View(jobPost);
                    }

                    _context.JobPosts.Add(jobPost);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            return View(jobPost);
        }











        // GET: JobPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts.FindAsync(id);
            if (jobPost == null)
            {
                return NotFound();
            }
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, JobPost jobPost)
        {
            if (id != jobPost.JobPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.JobPostId))
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
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .FirstOrDefaultAsync(m => m.JobPostId == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);
            if (jobPost != null)
            {
                _context.JobPosts.Remove(jobPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(int id)
        {
            return _context.JobPosts.Any(e => e.JobPostId == id);
        }
    }
}
