using Microsoft.EntityFrameworkCore;
using MarketCore.Models;

namespace MarketCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CustomerVendor> CustomersAndVendors { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreLog> StoreLogs { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<UnitName> UnitNames { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
     



    }
}
