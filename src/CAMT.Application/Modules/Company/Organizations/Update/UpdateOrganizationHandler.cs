using CAMT.Domain.Entities.Company.Interfaces;

namespace CAMT.Application.Modules.Company.Organizations.Update;

public class UpdateOrganizationHandler : IRequestHandler<UpdateOrganizationCmd, ErrorOr<Unit>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateOrganizationCmd request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            return Error.NotFound("Organization.NotFound", "The organization with the provide Id was not found.");
        }

        organization.Name = request.Name.ToUpper().Trim();

        await _organizationRepository.Update(organization);

        return Unit.Value;
    }
}