namespace MarketCore.Models
{
    public class CompanyInfo
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }

        public byte[]? Logo { get; set; }
    }
}
