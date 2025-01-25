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
    public class JobPostsController : Controller
    {
        private readonly AppDbContext _context;

        public JobPostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/JobPosts
        public async Task<IActionResult> Index() { 

        var jobPosts = await _context.JobPosts.ToListAsync();


        ViewBag.PendingJobs = jobPosts.Where(o => o.Status == JobPostStatus.Pending).ToList();
        ViewBag.AcceptedJobs = jobPosts.Where(o => o.Status == JobPostStatus.Accepted).ToList();
        ViewBag.RejectedJobs = jobPosts.Where(o => o.Status == JobPostStatus.Rejected).ToList();

            return View();
    }

    public async Task<IActionResult> Pend(int id)
    {
        var jobPosts = await _context.JobPosts.FindAsync(id);
        if (jobPosts == null)
        {
            return NotFound();
        }

            jobPosts.Status = JobPostStatus.Pending;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Accept(int id)
    {
        var jobPosts = await _context.JobPosts.FindAsync(id);
        if (jobPosts == null)
        {
            return NotFound();
        }

            jobPosts.Status = JobPostStatus.Accepted;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Reject(int id)
    {
        var jobPosts = await _context.JobPosts.FindAsync(id);
        if (jobPosts == null)
        {
            return NotFound();
        }

            jobPosts.Status = JobPostStatus.Rejected;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public ActionResult Details(int id)
    {
        var jobPosts = _context.JobPosts.Find(id);
        if (jobPosts == null)
        {
            return NotFound();
        }
        return View(jobPosts);
    }
}
}
