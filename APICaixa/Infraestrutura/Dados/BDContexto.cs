using APICaixa.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Dados
{
    public class BDContexto : DbContext
    {
        public BDContexto(DbContextOptions<BDContexto> options) : base(options) { }

        public DbSet<ContaBancaria> Contas { get; set; }
        public DbSet<LogTransferencia> LogsTransferencia { get; set; }
        public DbSet<LogDesativacaoConta> LogsDesativacaoConta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<ContaBancaria>()
                .HasIndex(c => c.Documento)
                .IsUnique();
        }
    }
}

