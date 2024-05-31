namespace CAMT.Application.Modules.Company.Organizations.Update;

public record UpdateOrganizationCmd : IRequest<ErrorOr<Unit>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}