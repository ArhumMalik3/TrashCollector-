﻿using System;
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
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public bool setPickUp()
        //{
        //    return false;
        //}
        // GET: Customers
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId ==
            userId).SingleOrDefault();
            //var customersAddress = _context.Addresses.Where(a => a.Id == )
            if (customer == null)
            {
                
                return RedirectToAction("Create");
                //try to get address if address is null redirect to address controller - make sure to preserve customer so we can tie them together
            }
            //I need to do a query to find that user id is in the db if null the call create method if not then set that customer equal to this one
            
            //else if ()
            //{
            //    return RedirectToAction("Create", "PickUps");
            //}
            
            
            //var applicationDbContext = _context.Customers.Include(c => c.Address).Include(c => c.IdentityUser);
            return View("Details", customer);
        }

        // GET: Customers/Details/5
        public IActionResult Details(Customer customer)
        {
            
            
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            Customer customer = new Customer();
            
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Create", "Addresses");
                
            }
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", customer.AddressId);
            
            return RedirectToAction("Details");
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (id == null)
            {
                return NotFound();
            }

            
            if (customer == null)
            {
                return NotFound();
            }
           
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            

            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customerFromDb = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();

                customerFromDb.firstName = customer.firstName;
                customerFromDb.lastName = customer.lastName;
                customerFromDb.weeklyPickUpDay = customer.weeklyPickUpDay;
                customerFromDb.startDate = customer.startDate;
                customerFromDb.endDate = customer.endDate;

                _context.Update(customerFromDb);
                _context.SaveChanges();
                return RedirectToAction("Details", customerFromDb);



            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
