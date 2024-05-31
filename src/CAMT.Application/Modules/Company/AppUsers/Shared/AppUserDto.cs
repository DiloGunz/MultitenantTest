namespace CAMT.Application.Modules.Company.AppUsers.Shared;

public record AppUserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public Guid OrganizationId { get; set; }
}