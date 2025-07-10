using Microsoft.AspNetCore.Mvc.Rendering;
using MarketCore.Models;

namespace MarketCore.ViewModels
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; } = new();
        public List<ProductUnit> Units { get; set; } = new();

        // اجعلها nullable بدون null!
        public SelectList? CategoryList { get; set; }
        public SelectList? UnitList { get; set; }
    }

}
