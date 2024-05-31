namespace CAMT.Domain.Utility;

public record Constants
{
    /// <summary>
    /// Claim name to insert the user's tenant into the JWT
    /// </summary>
    public const string TenantIdentifierClaim = "tenant";

    /// <summary>
    /// name of the parameter that is entered in the product controller route, which indicates the name of the TENANT to establish the connection to the corresponding database
    /// </summary>
    public const string NameParameterRouteTenant = "slugTenant";

    /// <summary>
    /// DbContext name for Organizations and users Database
    /// </summary>
    public const string CompanyDbContext = "Company";

    /// <summary>
    /// DBContext name for main product database
    /// </summary>
    public const string MainCatalogDbContext = "MainCatalog";

    /// <summary>
    /// DBContext name for product database for each Tenant
    /// </summary>
    public const string TenantCatalogDbContext = "TenantCatalog";
}