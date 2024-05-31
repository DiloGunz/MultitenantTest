namespace CAMT.Application.Modules.Company.Organizations.ValidateTenantUser;

public record ValidateTenantUserCmd : IRequest<ErrorOr<Unit>>
{
    public Guid IdUser { get; set; }
    public string TenanIdentifier { get; set; }
}