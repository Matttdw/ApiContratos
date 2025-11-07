using Microsoft.EntityFrameworkCore;
using ApiContratos.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiContratos.Data
{
    // Classe que representa o contexto do banco de dados da aplicação
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Propriedade que representa a tabela de contratos no banco de dados
        public DbSet<Contrato> Contratos { get; set; } = null!;

        // Configurações adicionais do modelo de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para converter DateOnly para string no formato "yyyy-MM-dd"
            var dateOnlyConverter = new ValueConverter<DateOnly, string>(
                d => d.ToString("yyyy-MM-dd"),
                s => DateOnly.Parse(s));

            // Aplica a conversão nos campos DataInicio e DataVencimento
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.Property(e => e.DataInicio).HasConversion(dateOnlyConverter);
                entity.Property(e => e.DataVencimento).HasConversion(dateOnlyConverter);
            });
        }
    }
}
