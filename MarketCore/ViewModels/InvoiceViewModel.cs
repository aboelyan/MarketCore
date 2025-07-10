using MarketCore.Models;

namespace MarketCore.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceHeader Header { get; set; } = new();
        public List<InvoiceDetail> Details { get; set; } = new();
        public int SelectedInvoiceType { get; set; }
    }
}
