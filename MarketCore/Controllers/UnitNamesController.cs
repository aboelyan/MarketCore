using MarketCore.Data;
using MarketCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketCore.Controllers
{
    public class UnitNamesController : Controller
    {
        private readonly AppDbContext _context;

        public UnitNamesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UnitNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitNames.ToListAsync());
        }

        // GET: UnitNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var unit = await _context.UnitNames.FirstOrDefaultAsync(m => m.ID == id);
            if (unit == null) return NotFound();

            return View(unit);
        }

        // GET: UnitNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitNames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitName unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: UnitNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var unit = await _context.UnitNames.FindAsync(id);
            if (unit == null) return NotFound();

            return View(unit);
        }

        // POST: UnitNames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnitName unit)
        {
            if (id != unit.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.UnitNames.Any(e => e.ID == id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: UnitNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var unit = await _context.UnitNames.FirstOrDefaultAsync(m => m.ID == id);
            if (unit == null) return NotFound();

            return View(unit);
        }

        // POST: UnitNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.UnitNames.FindAsync(id);
            if (unit != null)
            {
                _context.UnitNames.Remove(unit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}