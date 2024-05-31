using CAMT.Domain.Data;
using CAMT.Domain.Entities.Catalog;
using CAMT.Infrastructure.Persistence.Configuration.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Persistence.DbContexts;

public class CatalogDbContext : DbContext, ICatalogDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}