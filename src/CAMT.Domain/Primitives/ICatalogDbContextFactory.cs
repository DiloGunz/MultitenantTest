using CAMT.Domain.Data;

namespace CAMT.Domain.Primitives;

public interface ICatalogDbContextFactory
{
    ICatalogDbContext CreateDbContext();
    void ApplyMigrations();
}