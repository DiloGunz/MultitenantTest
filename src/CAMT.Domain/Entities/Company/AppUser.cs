using Microsoft.AspNetCore.Identity;

namespace CAMT.Domain.Entities.Company;

public class AppUser : IdentityUser<Guid>
{
    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// ID of the organization to which the user belongs
    /// </summary>
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
}