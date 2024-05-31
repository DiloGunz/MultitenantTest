using System.Net;
using System.Text.Json;

namespace CAMT.WebApi.Middlewares;

/// <summary>
/// Middleware to evaluate the 'slugTenant' and connect to the corresponding product database
/// </summary>
public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantMiddleware> _logger;
    private readonly TenantMiddlewareService _tenantMiddlewareService;

    public TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger, TenantMiddlewareService tenantMiddlewareService)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tenantMiddlewareService = tenantMiddlewareService ?? throw new ArgumentNullException(nameof(tenantMiddlewareService));
    }

    public async Task Invoke(HttpContext context)
    {
       
        try
        {
            string msg = _tenantMiddlewareService.ApplyTenant();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Tenat Error",
                    Title = "Tenant Error",
                    Detail = msg
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else
            {
                await _next(context);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the TenantMiddleware.");
            throw;
        }
    }
}
