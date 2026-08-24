using Microsoft.EntityFrameworkCore;
using POC_MessageBroker.Domains;

namespace POC_MessageBroker.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Transacao> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Transacao>(e =>
        {
            e.Property(x => x.Id).IsRequired();
            e.HasKey(x => x.Id);
        });

        base.OnModelCreating(mb);
    }
}
