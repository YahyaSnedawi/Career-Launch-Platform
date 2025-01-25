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
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var contacts = _context.Contacts
                .ToList();

            int totalContacts = contacts.Count;
            int totalPages = (int)Math.Ceiling(totalContacts / (double)pageSize);

            var paginatedContacts = contacts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(paginatedContacts);
        }
        public IActionResult Details(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id); 
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact); 
        }


        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Dashboard/Applications/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
