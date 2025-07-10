using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketCore.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Code { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        public string? Type { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? Image { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public ProductCategory? Category { get; set; }

        public ICollection<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();
    }


}
