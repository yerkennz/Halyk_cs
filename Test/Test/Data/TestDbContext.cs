using Microsoft.EntityFrameworkCore;
using CurrencyMvcApi.Models;

namespace CurrencyMvcApi.Data
{
    public class TestDbContext : DbContext
    {
        public DbSet<Currency> R_CURRENCY { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(3);
                entity.Property(e => e.Value).IsRequired().HasColumnType("numeric(18,2)");
                entity.Property(e => e.A_Date).IsRequired();
            });
        }
    }
}
