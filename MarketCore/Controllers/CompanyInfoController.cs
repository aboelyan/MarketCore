using Microsoft.AspNetCore.Mvc;
using MarketCore.Models;
using MarketCore.Data;
using Microsoft.EntityFrameworkCore;

namespace MarketCore.Controllers
{
    public class CompanyInfoController : Controller
    {
        private readonly AppDbContext _context;

        public CompanyInfoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit()
        {
            var info = await _context.CompanyInfo.FirstOrDefaultAsync();
            if (info == null)
                info = new CompanyInfo();

            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyInfo model, IFormFile? logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await logoFile.CopyToAsync(ms);
                model.Logo = ms.ToArray();
            }

            if (model.ID == 0)
                _context.CompanyInfo.Add(model);
            else
                _context.CompanyInfo.Update(model);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> GetLogo()
        {
            var logo = await _context.CompanyInfo.Select(c => c.Logo).FirstOrDefaultAsync();
            if (logo == null) return NotFound();
            return File(logo, "image/png");
        }
    }
}
