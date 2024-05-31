namespace CAMT.Application.Modules.Company.Organizations.Delete;

public class DeleteOrganizationCmdValidator : AbstractValidator<DeleteOrganizationCmd>
{
	public DeleteOrganizationCmdValidator()
	{
        RuleFor(r => r.Id).NotEmpty();
    }
}