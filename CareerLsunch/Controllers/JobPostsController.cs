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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public IActionResult Index(int page = 1, int pageSize = 6)
        {

            var totalPosts = _context.JobPosts
                .Where(o => o.Status == JobPostStatus.Accepted)
                .Count();


            var totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);


            var jobPosts = _context.JobPosts
                .Where(o => o.Status == JobPostStatus.Accepted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(jobPosts);
        }



        // GET: JobPosts/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            var jobPost = _context.JobPosts.FirstOrDefault(j => j.JobPostId == id);

            if (jobPost == null)
            {
                return NotFound(); 
            }

            return View(jobPost);
        }




        public IActionResult Application(int jobPostId)
        {
            var jobPost = _context.JobPosts.FirstOrDefault(j => j.JobPostId == jobPostId);

            if (jobPost == null)
            {
                return NotFound();
            }

            var application = new Application
            {
                JobPostId = jobPost.JobPostId,
              
            };

            return View(application);
        }



        [HttpPost]
        public IActionResult SubmitApplication(Application application, IFormFile ResumeFile)
        {
            if (ResumeFile != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ResumeFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ResumeFile.CopyTo(stream);
                }

                application.ResumePath = "/uploads/" + ResumeFile.FileName;
            }

            _context.Applications.Add(application);
            _context.SaveChanges();

            return RedirectToAction("Index", "JobPosts");
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
            if (!ModelState.IsValid)
            {
                return View(jobPost);
            }

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
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

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
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                Console.WriteLine($"Error: {ex.Message}");
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
