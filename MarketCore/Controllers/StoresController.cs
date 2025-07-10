using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketCore.Data;
using MarketCore.Models;

namespace MarketCore.Controllers
{
    public class StoresController : Controller
    {
        private readonly AppDbContext _context;

        public StoresController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Stores.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Store());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();
            return View(store);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Stores.Update(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();
            return View(store);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
