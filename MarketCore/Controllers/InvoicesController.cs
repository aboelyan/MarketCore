using MarketCore.Data;
using MarketCore.Enums;
using MarketCore.Models;
using MarketCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarketCore.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? type, DateTime? from, DateTime? to)
        {
            var query = _context.InvoiceHeaders.AsQueryable();

            if (type.HasValue)
                query = query.Where(i => (int)i.InvoiceType == type.Value);

            if (from.HasValue)
                query = query.Where(i => i.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(i => i.Date <= to.Value);

            var invoices = await query
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            ViewBag.Parties = await _context.CustomersAndVendors
                .Select(p => new { p.ID, p.Name })
                .ToListAsync();

            return View(invoices);
        }

        public IActionResult Create(int? invoiceType)
        {
            ViewBag.Items = new SelectList(_context.Products, "ID", "Name");
            ViewBag.Units = new SelectList(_context.UnitNames, "ID", "Name");

            // إعداد الأسعار حسب نوع الفاتورة
            ViewBag.ItemsWithPrices = _context.ProductUnits
      .Select(p => new ProductUnitDto
      {
          ProductID = p.ProductID,
          UnitID = p.UnitID,
          SellPrice = p.SellPrice,
          BuyPrice = p.BuyPrice
      }).ToList();


          

            // جلب الأطراف حسب نوع الفاتورة
            int vendorType = 1, customerType = 2;
            List<PartyDto> parties;

            if (invoiceType == (int)InvoiceTypes.Purchase || invoiceType == (int)InvoiceTypes.PurchaseReturn)
            {
                // موردين
                parties = _context.CustomersAndVendors
                    .Where(p => p.Type == vendorType)
                    .Select(p => new PartyDto { ID = p.ID, Name = p.Name, Type = p.Type })
                    .ToList();
            }
            else
            {
                // عملاء
                parties = _context.CustomersAndVendors
                    .Where(p => p.Type == customerType)
                    .Select(p => new PartyDto { ID = p.ID, Name = p.Name, Type = p.Type })
                    .ToList();
            }

            ViewBag.AllParties = parties;

            var vm = new InvoiceViewModel();
            if (invoiceType.HasValue)
            {
                vm.SelectedInvoiceType = invoiceType.Value; // يتم الحفظ مؤقتًا هنا
                vm.Header.InvoiceType = (InvoiceTypes)invoiceType.Value; // يتم حفظه هنا في الانم
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // تحويل النوع المختار إلى Enum
                vm.Header.InvoiceType = (InvoiceTypes)vm.SelectedInvoiceType;

                // الحسابات والحفظ كما وضعت من قبل...
                _context.InvoiceHeaders.Add(vm.Header);
                await _context.SaveChangesAsync();

                foreach (var detail in vm.Details)
                {
                    detail.InvoiceID = vm.Header.ID;
                    detail.TotalPrice = (detail.ItemQty * detail.Price) - detail.Discount;
                    detail.TotalCostValue = detail.ItemQty * detail.CostValue;
                    _context.InvoiceDetails.Add(detail);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // إعادة تحميل الـ ViewBags في حالة الخطأ
            ViewBag.Items = new SelectList(_context.Products, "ID", "Name");
            ViewBag.Units = new SelectList(_context.UnitNames, "ID", "Name");
            ViewBag.Suppliers = new SelectList(_context.CustomersAndVendors, "ID", "Name");

            return View(vm);
        }

    }

}
