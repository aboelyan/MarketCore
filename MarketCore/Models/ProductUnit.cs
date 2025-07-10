using System.ComponentModel.DataAnnotations.Schema;

namespace MarketCore.Models
{
    public class ProductUnit
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product? Product { get; set; }

        public int UnitID { get; set; }

        [ForeignKey(nameof(UnitID))]
        public UnitName? Unit { get; set; }

        public decimal Factor { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public decimal? SellDiscount { get; set; }

        public string? Barcode { get; set; }
    }


}
