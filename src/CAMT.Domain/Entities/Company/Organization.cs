using CAMT.Domain.Base;

namespace CAMT.Domain.Entities.Company;

public class Organization : EntityBase
{
    /// <summary>
    /// Organization name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Identify the organization with a single word, it is used for connection to the product database
    /// </summary>
    public string TenantIdentifier { get; set; }
}