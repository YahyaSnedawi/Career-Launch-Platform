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
    public class PortfoliosController : Controller
    {
        private readonly AppDbContext _context;

        public PortfoliosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Portfolios
        public async Task<IActionResult> Index() 
        { 
 var portfolios = await _context.Portfolios.ToListAsync();


        ViewBag.PendingPortfolios = portfolios.Where(o => o.Status == PortfolioStatus.Pending).ToList();
        ViewBag.AcceptedPortfolios = portfolios.Where(o => o.Status == PortfolioStatus.Accepted).ToList();
        ViewBag.RejectedPortfolios = portfolios.Where(o => o.Status == PortfolioStatus.Rejected).ToList();

            return View();
    }

    public async Task<IActionResult> Pend(int id)
    {
        var portfolio = await _context.Portfolios.FindAsync(id);
        if (portfolio == null)
        {
            return NotFound();
        }

        portfolio.Status = PortfolioStatus.Pending;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Accept(int id)
    {
        var portfolio = await _context.Portfolios.FindAsync(id);
        if (portfolio == null)
        {
            return NotFound();
        }

        portfolio.Status = PortfolioStatus.Accepted;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Reject(int id)
    {
        var portfolio = await _context.Portfolios.FindAsync(id);
        if (portfolio == null)
        {
            return NotFound();
        }

        portfolio.Status = PortfolioStatus.Rejected;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public ActionResult Details(int id)
    {
        var portfolio = _context.Portfolios.Find(id);
        if (portfolio == null)
        {
            return NotFound();
        }
        return View(portfolio);
    }
}
}