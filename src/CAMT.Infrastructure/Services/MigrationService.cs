using CAMT.Domain.Interfaces;
using CAMT.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CAMT.Infrastructure.Services;

public class MigrationService : IMigrationService
{
    private readonly IConnectionStringProvider _connectionStringProvider;
    private readonly ILogger<MigrationService> _logger;

    public MigrationService(IConnectionStringProvider connectionStringProvider, ILogger<MigrationService> logger)
    {
        _connectionStringProvider = connectionStringProvider ?? throw new ArgumentNullException(nameof(connectionStringProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void ApplyMigrations(string tenantIdentifier)
    {
        var connectionString = _connectionStringProvider.GetConnectionString(tenantIdentifier);
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using (var context = new CatalogDbContext(optionsBuilder.Options))
        {
            var migrator = context.Database.GetService<IMigrator>();

            var pendingMigrations = context.Database.GetPendingMigrations().ToList();
            if (pendingMigrations.Any())
            {
                foreach (var migration in pendingMigrations)
                {
                    _logger.LogInformation($"Applying migration: {migration}");
                    migrator.Migrate(migration);
                }
            }
        }
    }
}