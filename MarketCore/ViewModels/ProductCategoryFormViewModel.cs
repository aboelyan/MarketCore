using MarketCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketCore.ViewModels
{
    public class ProductCategoryFormViewModel
    {
        public ProductCategory Category { get; set; }

        public List<SelectListItem> ParentCategories { get; set; } = new();
    }
}
