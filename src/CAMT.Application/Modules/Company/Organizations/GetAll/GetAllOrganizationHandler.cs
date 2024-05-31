using CAMT.Application.Modules.Company.Organizations.Shared;
using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;

namespace CAMT.Application.Modules.Company.Organizations.GetAll;

public class GetAllOrganizationHandler : IRequestHandler<GetAllOrganizationQuery, ErrorOr<IReadOnlyList<OrganizationDto>>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetAllOrganizationHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<OrganizationDto>>> Handle(GetAllOrganizationQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Organization> listResult = await _organizationRepository.GetAllAsync();

        return listResult.Select(x => new OrganizationDto()
        {
            Id = x.Id,
            Name = x.Name,
            TenantIdentifier = x.TenantIdentifier,
        }).OrderBy(x => x.Name).ToList();
    }
}