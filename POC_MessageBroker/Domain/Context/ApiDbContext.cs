using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Conta>(e =>
        {
            e.Property(x => x.Id).IsRequired();
            e.HasKey(x => x.Id);
        });

        mb.Entity<Transacao>(e =>
        {
            e.Property(x => x.Id).IsRequired();
            e.HasKey(x => x.Id);

            e.HasIndex(x => x.CodigoId)
                .IsUnique();

            e.HasOne(x => x.Conta)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.ContaId);

            e.Property(x => x.DataHora)
                .HasColumnType("timestamp without time zone");
        });

        base.OnModelCreating(mb);
    }
}
