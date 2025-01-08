using CareerLaunch.Data;
using CareerLaunch.Models;
using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CareerLaunch.Controllers
{
  
    public class ApplicationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> ApplyForJob(int jobId, string coverLetter, IFormFile resumeFile)
        {
            var jobPost = await _context.JobPosts
                .FirstOrDefaultAsync(j => j.JobPostId == jobId);

            if (jobPost == null)
            {
                return NotFound();  
            }
            
            var jobSeekerId = _userManager.GetUserId(User);  
            

            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobSeekerId == jobSeekerId && a.JobId == jobId);

            if (existingApplication != null)
            {
                TempData["Message"] = "You have already applied for this job.";
                return RedirectToAction("Details", "JobPost", new { id = jobId });
            }

            string resumePath = null;
            if (resumeFile != null && resumeFile.Length > 0)
            {
                var fileName = Path.GetFileName(resumeFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await resumeFile.CopyToAsync(fileStream);
                }

                resumePath = "/resumes/" + fileName;
            }

            var application = new Application
            {
                JobId = jobId,
                JobSeekerId = jobSeekerId,
                EmployerId = jobPost.EmployerId,
                CoverLetter = coverLetter,
                ResumePath = resumePath,  
                Status = ApplicationStatus.Pending,
                ApplicationDate = DateTime.Now
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your application has been submitted successfully!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Details", "JobPost", new { id = jobId });
        }
    }
}
