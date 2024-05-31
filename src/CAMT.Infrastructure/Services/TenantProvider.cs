using CAMT.Domain.Interfaces;

namespace CAMT.Infrastructure.Services;

public class TenantProvider : ITenantProvider
{
    private readonly IRequestContext _requestContext;

    public TenantProvider(IRequestContext requestContext)
    {
        _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
    }

    public string GetTenantIdentifier()
    {
        return _requestContext.GetTenantIdentifier();
    }
}