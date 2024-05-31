namespace CAMT.Application.Modules.Company.Organizations.Create;

public record CreateOrganizationCmd : IRequest<ErrorOr<Guid>>
{
    public string Name { get; set; }
    public string TenantIdentifier { get; set; }
}