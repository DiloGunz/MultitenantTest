using CAMT.Application.Utility;

namespace CAMT.Application.Modules.Company.Organizations.Create;

public class CreateOrganizationCmdValidator : AbstractValidator<CreateOrganizationCmd>
{
    public CreateOrganizationCmdValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TenantIdentifier).Must(x => TenantIdentifierValidate.Validate(x)).WithMessage("Incorrect format");
    }
}