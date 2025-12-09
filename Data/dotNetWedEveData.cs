using Microsoft.EntityFrameworkCore;

namespace dotNetWedEve.Data
{
    public class dotNetWedEveContext : DbContext
    {
        public dotNetWedEveContext(DbContextOptions options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
