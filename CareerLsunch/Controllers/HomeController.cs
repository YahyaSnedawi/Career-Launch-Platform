using CareerLaunch.Data;
using CareerLaunch.Models;
using CareerLaunch.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CareerLaunch.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Applications = _context.Applications.Count();
            ViewBag.JobPosts = _context.JobPosts.Where(p=>p.Status == JobPostStatus.Accepted).Count();
            ViewBag.Portfolios = _context.Portfolios.Where(p => p.Status == PortfolioStatus.Accepted).Count();
            ViewBag.BlogPosts = _context.Blogs.Where(p => p.Status == BlogPostStatus.Accepted).Count();



            var jobPosts = _context.JobPosts
                .Where(o => o.Status == JobPostStatus.Accepted)
    .ToList();

            return View(jobPosts);
        }
        [HttpPost]
        public IActionResult SubmitContactForm(Contact model)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(model);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Contact");
            }
            TempData["ErrorMessage"] = "There was an error. Please try again.";
            return View("Contact", model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
