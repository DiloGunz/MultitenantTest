using CAMT.Domain.Interfaces;

namespace CAMT.Infrastructure.Services;

public class RequestContext : IRequestContext
{
    private string _tenantIdentifier = "";

    public string GetTenantIdentifier()
    {
        return _tenantIdentifier;
    }

    public void SetTenantIdentifier(string value)
    {
        _tenantIdentifier = value;
    }
}