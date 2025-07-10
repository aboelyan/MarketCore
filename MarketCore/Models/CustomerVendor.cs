namespace MarketCore.Models
{
    public class CustomerVendor
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public int Type { get; set; } // 1 = مورد، 2 = عميل
        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public int? AccountID { get; set; }

        public bool IsCustomer { get; set; }  // true = عميل، false = مورد
    }
}
