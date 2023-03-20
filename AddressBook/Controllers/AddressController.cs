using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressContext _context;

        public AddressController(AddressContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index(int? id)
        {
            return View(await _context.AddressClasses.ToListAsync());
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddressClasses == null)
            {
                return NotFound();
            }

            var addressClass = await _context.AddressClasses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addressClass == null)
            {
                return NotFound();
            }

            return View(addressClass);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,PhoneNumber,StreetAddress,City,State,PostalCode")] AddressClass addressClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressClass);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddressClasses == null)
            {
                return NotFound();
            }

            var addressClass = await _context.AddressClasses.FindAsync(id);
            if (addressClass == null)
            {
                return NotFound();
            }
            return View(addressClass);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,PhoneNumber,StreetAddress,City,State,PostalCode")] AddressClass addressClass)
        {
            if (id != addressClass.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressClassExists(addressClass.ID))
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
            return View(addressClass);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddressClasses == null)
            {
                return NotFound();
            }

            var addressClass = await _context.AddressClasses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addressClass == null)
            {
                return NotFound();
            }

            return View(addressClass);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddressClasses == null)
            {
                return Problem("Entity set 'AddressContext.AddressClasses'  is null.");
            }
            var addressClass = await _context.AddressClasses.FindAsync(id);
            if (addressClass != null)
            {
                _context.AddressClasses.Remove(addressClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressClassExists(int id)
        {
          return (_context.AddressClasses?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
