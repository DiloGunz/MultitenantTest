using CAMT.Application.Modules.Company.Organizations.Shared;
using CAMT.Domain.Entities.Company.Interfaces;

namespace CAMT.Application.Modules.Company.Organizations.GetById;

public class GetByIdOrganizationHandler : IRequestHandler<GetByIdOrganizationQuery, ErrorOr<OrganizationDto>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetByIdOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public async Task<ErrorOr<OrganizationDto>> Handle(GetByIdOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            return Error.NotFound("Organization.NotFound", "The organization with the provide Id was not found.");
        }

        return new OrganizationDto()
        {
            Id = organization.Id,
            Name = organization.Name,
            TenantIdentifier = organization.TenantIdentifier,
        };
    }
}