using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketCore.Data;
using MarketCore.Models;

namespace MarketCore.Controllers
{
    public class DrawersController : Controller
    {
        private readonly AppDbContext _context;

        public DrawersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var drawers = await _context.Drawers.ToListAsync();
            return View(drawers);
        }

        public IActionResult Create()
        {
            return View(new Drawer());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Drawer model)
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
            var drawer = await _context.Drawers.FindAsync(id);
            if (drawer == null)
                return NotFound();

            return View(drawer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Drawer model)
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
            var drawer = await _context.Drawers.FindAsync(id);
            if (drawer == null)
                return NotFound();

            return View(drawer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drawer = await _context.Drawers.FindAsync(id);
            if (drawer != null)
            {
                _context.Drawers.Remove(drawer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
