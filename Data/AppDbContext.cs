using Microsoft.EntityFrameworkCore;
using ApiContratos.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiContratos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contrato> Contratos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateOnlyConverter = new ValueConverter<DateOnly, string>(
                d => d.ToString("yyyy-MM-dd"),
                s => DateOnly.Parse(s));

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.Property(e => e.DataInicio).HasConversion(dateOnlyConverter);
                entity.Property(e => e.DataVencimento).HasConversion(dateOnlyConverter);
            });
        }
    }
}
