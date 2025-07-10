namespace MarketCore.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? SalesAccountID { get; set; }
        public int? SalesReturnAccountID { get; set; }
        public int? InventoryAccountID { get; set; }
        public int? CostOfSoldGoodsAccountID { get; set; }
        public int? DiscountAllowedAccountID { get; set; }
        public int? DiscountReceivedAccountID { get; set; }
    }
}
