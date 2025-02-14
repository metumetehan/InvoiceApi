using Microsoft.EntityFrameworkCore;

namespace InvoiceApi.Data
{
    public class TaxDbContext : DbContext
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> options) : base(options) { }

        public DbSet<Tax> Taxes { get; set; } // Add your entity model here
    }
}
