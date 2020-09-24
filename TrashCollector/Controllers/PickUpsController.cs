using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrashCollector.Data;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class PickUpsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PickUpsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PickUps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PickUps.Include(p => p.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PickUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUp = await _context.PickUps
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickUp == null)
            {
                return NotFound();
            }

            return View(pickUp);
        }

        // GET: PickUps/Create
        public IActionResult Create()
        {
            PickUp pickUp = new PickUp();
            return View(pickUp);
        }

        // POST: PickUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PickUp pickUp)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(pickUp);
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = _context.Customers.Where(c => c.IdentityUserId == userId).First();
                pickUp.Customer = customer;
                pickUp.pickedUp = false;

                _context.PickUps.Update(pickUp);
                _context.SaveChanges();

                return RedirectToAction("Index", "Customers");
            }
            return RedirectToAction("Index", "Customers");
        }

        // GET: PickUps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUp = await _context.PickUps.FindAsync(id);
            if (pickUp == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", pickUp.CustomerId);
            return View(pickUp);
        }

        // POST: PickUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,pickedUp,timeOfPickup")] PickUp pickUp)
        {
            if (id != pickUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pickUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PickUpExists(pickUp.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", pickUp.CustomerId);
            return View(pickUp);
        }

        // GET: PickUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickUp = await _context.PickUps
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickUp == null)
            {
                return NotFound();
            }

            return View(pickUp);
        }

        // POST: PickUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickUp = await _context.PickUps.FindAsync(id);
            _context.PickUps.Remove(pickUp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PickUpExists(int id)
        {
            return _context.PickUps.Any(e => e.Id == id);
        }
    }
}
