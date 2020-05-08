using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using contacts_rssu.Models;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace contacts_rssu.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext _context;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger, ContactContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string name, string email) // Возможно стоит сделать partial view / component view view 
        {
            IQueryable<Contact> contacts = _context.Contacts;

            if (!String.IsNullOrEmpty(name))
            {
                _logger.LogInformation("Зашли в if(!String.IsNullOrEmpty(name))");
                contacts = contacts.Where(c => c.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(email))
            {
                _logger.LogInformation("Зашли в if(!String.IsNullOrEmpty(email))");
                contacts = contacts.Where(p => p.Email.Contains(email));
            }

            return View(contacts);
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Зашли в метод Delete"); //error log

            var contact = _context.Contacts.Find(id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
