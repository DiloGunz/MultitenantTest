namespace CAMT.Application.Modules.Company.Organizations.Shared;

public record OrganizationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TenantIdentifier { get; set; }
}