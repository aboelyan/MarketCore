using MarketCore.Data;
using MarketCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarketCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                            .Include(p => p.Category)
                            .Include(p => p.ProductUnits)
                                .ThenInclude(u => u.Unit)
                            .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            var lastCode = _context.Products
                .OrderByDescending(p => p.ID)
                .Select(p => p.Code)
                .FirstOrDefault();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(lastCode) && lastCode.StartsWith("PRD-"))
            {
                var numPart = lastCode.Substring(4);
                if (int.TryParse(numPart, out int num))
                    nextNumber = num + 1;
            }

            var vm = new ProductFormViewModel
            {
                Product = new Models.Product { Code = $"PRD-{nextNumber:D4}" },
                CategoryList = new SelectList(_context.ProductCategories.ToList(), "ID", "Name"),
                UnitList = new SelectList(_context.UnitNames.ToList(), "ID", "Name")
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(vm.Product);
                _context.SaveChanges();

                foreach (var unit in vm.Units)
                {
                    if (unit.UnitID > 0)
                    {
                        unit.ProductID = vm.Product.ID;
                        _context.ProductUnits.Add(unit);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // ❗ ضروري جدًا: إعادة تعبئة القوائم عند الخطأ
            vm.CategoryList = new SelectList(_context.ProductCategories.ToList(), "ID", "Name");
            vm.UnitList = new SelectList(_context.UnitNames.ToList(), "ID", "Name");
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductUnits)
                .FirstOrDefault(p => p.ID == id);

            if (product == null)
                return NotFound();

            var vm = new ProductFormViewModel
            {
                Product = product,
                Units = product.ProductUnits.ToList(),
                CategoryList = new SelectList(_context.ProductCategories.ToList(), "ID", "Name"),
                UnitList = new SelectList(_context.UnitNames.ToList(), "ID", "Name")
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductFormViewModel vm)
        {
            if (id != vm.Product.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                // تحديث المنتج
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _context.Update(vm.Product);
                _context.SaveChanges();

                // حذف الوحدات القديمة
                var existingUnits = _context.ProductUnits.Where(u => u.ProductID == id);
                _context.ProductUnits.RemoveRange(existingUnits);
                _context.SaveChanges();

                // إضافة الوحدات الجديدة
                foreach (var unit in vm.Units)
                {
                    if (unit.UnitID > 0)
                    {
                        unit.ProductID = id;
                        _context.ProductUnits.Add(unit);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // في حالة الخطأ
            vm.CategoryList = new SelectList(_context.ProductCategories.ToList(), "ID", "Name");
            vm.UnitList = new SelectList(_context.UnitNames.ToList(), "ID", "Name");
            return View(vm);
        }

    }

}
