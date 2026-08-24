using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Transacao>(e =>
        {
            e.Property(x => x.Id).IsRequired();
            e.HasKey(x => x.Id);

            e.HasIndex(x => x.CodigoId)
                .IsUnique();
        });

        base.OnModelCreating(mb);
    }
}
