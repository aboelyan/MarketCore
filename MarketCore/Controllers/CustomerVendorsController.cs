using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketCore.Data;
using MarketCore.Models;

namespace MarketCore.Controllers
{
    public class CustomerVendorsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerVendorsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(bool? isCustomer)
        {
            var list = await _context.CustomersAndVendors
                .Where(c => !isCustomer.HasValue || c.IsCustomer == isCustomer.Value)
                .ToListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new CustomerVendor());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerVendor model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.CustomersAndVendors.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVendor model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CustomersAndVendors.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.CustomersAndVendors.FindAsync(id);
            if (item != null)
            {
                _context.CustomersAndVendors.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.CustomersAndVendors.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
