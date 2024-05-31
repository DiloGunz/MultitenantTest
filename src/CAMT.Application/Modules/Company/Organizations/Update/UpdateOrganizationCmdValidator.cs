using CAMT.Application.Utility;

namespace CAMT.Application.Modules.Company.Organizations.Update;

public class UpdateOrganizationCmdValidator : AbstractValidator<UpdateOrganizationCmd>
{
	public UpdateOrganizationCmdValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
		RuleFor(x => x.Name).NotEmpty();
	}
}