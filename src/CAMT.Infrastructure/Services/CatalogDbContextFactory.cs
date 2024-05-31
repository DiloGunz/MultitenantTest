using CAMT.Domain.Data;
using CAMT.Domain.Interfaces;
using CAMT.Domain.Primitives;
using CAMT.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Services;

public class CatalogDbContextFactory : ICatalogDbContextFactory
{
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly ITenantProvider _tenantProvider;
    private readonly IMigrationService _migrationService;

    public CatalogDbContextFactory(IConnectionStringProvider connectionStringProvider, ITenantProvider tenantProvider, IMigrationService migrationService)
    {
        _connectionStringProvider = connectionStringProvider ?? throw new ArgumentNullException(nameof(connectionStringProvider));
        _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
        _migrationService = migrationService ?? throw new ArgumentNullException(nameof(migrationService));
    }

    public void ApplyMigrations()
    {
        var tenantIdentifier = _tenantProvider.GetTenantIdentifier();

        _migrationService.ApplyMigrations(tenantIdentifier);
    }

    public ICatalogDbContext CreateDbContext()
    {
        var tenantIdentifier = _tenantProvider.GetTenantIdentifier();

        var connectionString = _connectionStringProvider.GetConnectionString(tenantIdentifier);

        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CatalogDbContext(optionsBuilder.Options);
    }
}