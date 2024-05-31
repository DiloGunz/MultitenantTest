namespace CAMT.Application.Modules.Company.AppUsers.Create;

public record CreateAppUserCmd : IRequest<ErrorOr<Guid>>
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public Guid OrganizationId { get; set; }
}