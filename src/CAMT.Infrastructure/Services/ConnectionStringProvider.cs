using CAMT.Domain.Interfaces;
using CAMT.Domain.Utility;
using Microsoft.Extensions.Configuration;

namespace CAMT.Infrastructure.Services;

public class ConnectionStringProvider : IConnectionStringProvider
{
    private readonly IConfiguration _configuration;

    public ConnectionStringProvider(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GetConnectionString(string tenantIdentifier)
    {
        var connectionStringTemplate = _configuration.GetConnectionString(Constants.TenantCatalogDbContext) ?? throw new ArgumentNullException("ConnectionString:Constants.TenantCatalogDbContext");
        return connectionStringTemplate.Replace("{tenant}", tenantIdentifier);
    }
}