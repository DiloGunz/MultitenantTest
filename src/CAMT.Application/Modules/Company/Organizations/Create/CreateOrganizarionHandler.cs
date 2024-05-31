using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;
using CAMT.Domain.Interfaces;
using CAMT.Domain.Primitives;

namespace CAMT.Application.Modules.Company.Organizations.Create;

/// <summary>
/// This handler creates an organization and at the same time creates a product database with the TenantIdentifier property prefix
/// </summary>
public class CreateOrganizarionHandler : IRequestHandler<CreateOrganizationCmd, ErrorOr<Guid>>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly ICatalogDbContextFactory _catalogDbContextFactory;
    private readonly IRequestContext _requestContext;

    public CreateOrganizarionHandler(IOrganizationRepository organizationRepository, ICatalogDbContextFactory catalogDbContextFactory, IRequestContext requestContext)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _catalogDbContextFactory = catalogDbContextFactory ?? throw new ArgumentNullException(nameof(catalogDbContextFactory));
        _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext)); 
    }

    public async Task<ErrorOr<Guid>> Handle(CreateOrganizationCmd request, CancellationToken cancellationToken)
    {
        var organizationEntity = new Organization()
        {
            Name = request.Name.ToUpper().Trim(),
            TenantIdentifier = request.TenantIdentifier.ToUpper().Trim(),
        };

        var existOrganizationName = await _organizationRepository.ExistNameAsync(organizationEntity.Name);

        if (existOrganizationName)
        {
            return Error.Conflict("The organization name already exist");
        }

        var existTenantIdentifier = await _organizationRepository.ExistTenantIdentifierAsync(organizationEntity.TenantIdentifier);

        if (existTenantIdentifier)
        {
            return Error.Conflict("The tenant identifier already exist");
        }

        await _organizationRepository.AddAsync(organizationEntity);

        _requestContext.SetTenantIdentifier(organizationEntity.TenantIdentifier);
        _catalogDbContextFactory.ApplyMigrations();

        return organizationEntity.Id;
    }
}