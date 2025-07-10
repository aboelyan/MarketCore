using MarketCore.Enums;

namespace MarketCore.Models
{
    public class InvoiceHeader
    {
        public int ID { get; set; }
        public InvoiceTypes InvoiceType { get; set; }  // بدل int
        public string? Code { get; set; }
     
        public int? PartType { get; set; }
        public int? PartID { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DelivaryDate { get; set; }
        public string? Notes { get; set; }

        public decimal Total { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountRation { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxValue { get; set; }
        public decimal Expences { get; set; }
        public decimal Net { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaing { get; set; }
   
        public int? Darwer { get; set; } // Store or Cash Drawer
        public string? ShippingAddress { get; set; }

        public bool PostedToStore { get; set; }
        public DateTime? PostDate { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; } = new();
    }
}
