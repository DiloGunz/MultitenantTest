using CAMT.Domain.Interfaces;
using CAMT.Domain.Utility;
using System.Security.Claims;

namespace CAMT.WebApi.Middlewares;

/// <summary>
/// Service to set the entered tenant identifier
/// </summary>
public class TenantMiddlewareService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestContext _requestContext;

    public TenantMiddlewareService(IHttpContextAccessor httpContextAccessor, IRequestContext requestContext)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
    }

    public string ApplyTenant()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            var routeValues = context.Request.RouteValues;
            var tenantIdentifier = routeValues[Constants.NameParameterRouteTenant] as string;
            if (!string.IsNullOrWhiteSpace(tenantIdentifier))
            {
                if (tenantIdentifier.ToLower() != GetTenantIdentifierByUserLogin().ToLower())
                {
                    return "The name of the organization admitted does not match the one that the user has";
                }
                else
                {
                    _requestContext.SetTenantIdentifier(tenantIdentifier ?? throw new ArgumentNullException(Constants.NameParameterRouteTenant));
                }
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Returns the tenant value from the JWT
    /// </summary>
    /// <returns></returns>
    private string GetTenantIdentifierByUserLogin()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user != null)
        {
            var identity = user.Identity;
            var claimTenant = (identity as ClaimsIdentity)!.FindFirst(Constants.TenantIdentifierClaim);
            if (claimTenant != null)
            {
                return claimTenant.Value;
            }
        }

        return string.Empty;
    }
}