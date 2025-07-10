namespace MarketCore.Models
{
    public class StoreLog
    {
        public int ID { get; set; }
        public string SourceType { get; set; } // "Purchase", "Sale", ...
        public int SourceID { get; set; } // رقم الفاتورة

        public int ProductID { get; set; }
        public int StoreID { get; set; }

        public decimal Qty { get; set; } // موجب أو سالب حسب النوع
        public bool IsInTransaction { get; set; } = true;

        public decimal CostValue { get; set; } // سعر التكلفة
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
}
