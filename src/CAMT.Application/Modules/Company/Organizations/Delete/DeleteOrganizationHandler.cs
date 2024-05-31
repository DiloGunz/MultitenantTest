
using CAMT.Domain.Entities.Company.Interfaces;

namespace CAMT.Application.Modules.Company.Organizations.Delete;

public class DeleteOrganizationHandler : IRequestHandler<DeleteOrganizationCmd, ErrorOr<Unit>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteOrganizationCmd request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            return Error.NotFound("Organization.NotFound", "The organization with the provide Id was not found.");
        }

        await _organizationRepository.Delete(organization);

        return Unit.Value;
    }
}