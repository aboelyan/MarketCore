using System.ComponentModel.DataAnnotations.Schema;

namespace MarketCore.Models
{
    public class InvoiceDetail
    {
        public int ID { get; set; }
     
        public int ItemID { get; set; }
        public int? ItemUnitID { get; set; }
        public decimal ItemQty { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal CostValue { get; set; }
        public decimal TotalCostValue { get; set; }

        public decimal Discount { get; set; }
        public decimal DiscountValue { get; set; }

        public int StoreID { get; set; }

        public int InvoiceID { get; set; }  // بدلاً من InoiceID

        [ForeignKey("InvoiceID")]
        public InvoiceHeader? InvoiceHeader { get; set; }

        [ForeignKey("ItemUnitID")]
        public UnitName? UnitName { get; set; }
    }
}
