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
    public class EmployeesController : Controller
    {
        public double pickUpFee = 49;
        private readonly ApplicationDbContext _context;
     

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Address> GetAddresses()
        {
            List<Address> allAddresses = _context.Addresses.ToList();

            return allAddresses;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> allCustomers = _context.Customers.ToList();

            return allCustomers;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context.Employees.Where(e => e.IdentityUserId ==
            userId).SingleOrDefault();
            if (employee == null)
            {
                RedirectToAction("Create");
            }
            //I need to do a query to find that user id is in the db if null the call create method if not then set that customer equal to this one
            var today = DateTime.Today.DayOfWeek.ToString();
            List<Customer> customerPickUps = null;
            List<Customer> customersInDb = GetCustomers();
            var zipCodesInArea = customersInDb.SkipWhile(c => c.Address.zipCode != employee.zipCode).ToList();
            
            customerPickUps = zipCodesInArea.Where(c => c.weeklyPickUpDay.ToString() == today).ToList();
             
            
            return View(customerPickUps);
        }

        public IActionResult Filter()
        {
            return View();
        }
        public IActionResult ConfirmPickUp(Customer customer)
        {
            //put this as a button on the drop down menu next to the customer in the filters
            
            var pickUp = _context.PickUps.Where(p => p.Customer == customer).First();
            
            pickUp.pickedUp = true;

            _context.PickUps.Update(pickUp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult ViewMap(int? id)
        {
            return View();
        }
        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View();
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            
            if (ModelState.IsValid)
            {

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee.IdentityUserId = userId;
                _context.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
               
            }
            
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            var pickUp = _context.PickUps.Where(p => p.CustomerId == id).Single();
            return View(pickUp);
            
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PickUp pickUp)
        {
            try
            {
                if (pickUp.pickedUp == true)
                {
                    pickUp.Customer.amountOwed+= pickUpFee;
                }
                _context.Update(pickUp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
