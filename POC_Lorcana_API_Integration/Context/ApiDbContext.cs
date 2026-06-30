using Microsoft.EntityFrameworkCore;
using POC_Lorcana_API_Integration.Domains;

namespace POC_Lorcana_API_Integration.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Card>(e =>
        {
            e.Property(x => x.Id).IsRequired();
            e.HasKey(x => x.Id);
        });

        base.OnModelCreating(mb);
    }
}
