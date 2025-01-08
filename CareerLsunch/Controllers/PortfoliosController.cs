using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerLaunch.Data;
using CareerLaunch.Models;

namespace CareerLaunch.Controllers
{
    public class PortfoliosController : Controller
    {
        private readonly AppDbContext _context;



        public PortfoliosController(AppDbContext context)
        {
            _context = context;

        }

        // GET: Portfolios
        public IActionResult Index(int page = 1, int pageSize = 9)
        {
            var portfolios = _context.Portfolios
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPosts = _context.Portfolios.Count();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            return View(portfolios);
        }

        // GET: Portfolios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(m => m.PortfolioId == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // GET: Portfolios/Create
        public IActionResult Create()
        {
            var portfolio = new Portfolio();
            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portfolio model)
        {
            if (ModelState.IsValid)
            {
                
                var profilePicturesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile_pictures");
                var resumesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resumes");

                if (!Directory.Exists(profilePicturesDirectory))
                {
                    Directory.CreateDirectory(profilePicturesDirectory);
                }

                if (!Directory.Exists(resumesDirectory))
                {
                    Directory.CreateDirectory(resumesDirectory);
                }

                
                if (model.ProfilePictureFile != null && model.ProfilePictureFile.Length > 0)
                {
                    var profilePicturePath = Path.Combine(profilePicturesDirectory, model.ProfilePictureFile.FileName);

                    using (var stream = new FileStream(profilePicturePath, FileMode.Create))
                    {
                        await model.ProfilePictureFile.CopyToAsync(stream);
                    }

                   
                    model.ProfilePicture = $"/images/profile_pictures/{model.ProfilePictureFile.FileName}";
                }

                
                if (model.ResumeFile != null && model.ResumeFile.Length > 0)
                {
                    var resumePath = Path.Combine(resumesDirectory, model.ResumeFile.FileName);

                    using (var stream = new FileStream(resumePath, FileMode.Create))
                    {
                        await model.ResumeFile.CopyToAsync(stream);
                    }

                    
                    model.ResumeUrl = $"/resumes/{model.ResumeFile.FileName}";
                }

                
                _context.Portfolios.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public IActionResult Edit()
        {
            return View();
        }

        // POST: Portfolios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Portfolio portfolio)
        {
            if (id != portfolio.PortfolioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.PortfolioId))
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
            return View(portfolio);
        }

        // GET: Portfolios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(m => m.PortfolioId == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.PortfolioId == id);
        }
    }
}
