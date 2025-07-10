namespace MarketCore.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductCategory
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }  // ✅ هذه السطر مهم جدًا

        public string Name { get; set; }

        public string? Number { get; set; }

        // (اختياري) علاقة التنقل
        public ProductCategory? Parent { get; set; }
    }

}
