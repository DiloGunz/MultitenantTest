namespace CAMT.Application.Modules.Company.Organizations.ValidateTenantUser;

public class ValidateTenantUserCmdValidator : AbstractValidator<ValidateTenantUserCmd>
{
	public ValidateTenantUserCmdValidator()
	{
		RuleFor(x => x.IdUser).NotEmpty();
		RuleFor(x => x.TenanIdentifier).NotEmpty();
	}
}