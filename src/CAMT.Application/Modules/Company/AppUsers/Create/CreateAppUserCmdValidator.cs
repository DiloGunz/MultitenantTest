namespace CAMT.Application.Modules.Company.AppUsers.Create;

public class CreateAppUserCmdValidator : AbstractValidator<CreateAppUserCmd>
{
    public CreateAppUserCmdValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
    }
}