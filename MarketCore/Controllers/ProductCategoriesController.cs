using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketCore.Data;
using MarketCore.Models;
using MarketCore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketCore.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public ProductCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return View(categories);
        }

        // GET
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductCategoryFormViewModel
            {
                ParentCategories = await _context.ProductCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Add(viewModel.Category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // إعادة تحميل القائمة لو حصل خطأ
            viewModel.ParentCategories = await _context.ProductCategories
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            var parentList = await _context.ProductCategories
                .Where(c => c.ID != id) // لا تسمح باختيار نفسه كفئة أم
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToListAsync();

            var viewModel = new ProductCategoryFormViewModel
            {
                Category = category,
                ParentCategories = parentList
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Update(viewModel.Category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // في حالة وجود خطأ، أعد تحميل القائمة المنسدلة
            viewModel.ParentCategories = await _context.ProductCategories
                .Where(c => c.ID != viewModel.Category.ID)
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                }).ToListAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category != null)
            {
                _context.ProductCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
    }
}

