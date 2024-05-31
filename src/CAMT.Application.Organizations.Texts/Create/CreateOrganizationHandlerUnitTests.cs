using CAMT.Application.Modules.Company.Organizations.Create;
using CAMT.Domain.Entities.Company.Interfaces;
using CAMT.Domain.Interfaces;
using CAMT.Domain.Primitives;

namespace CAMT.Application.Organizations.Texts.Create;

/// <summary>
/// 
/// </summary>
public class CreateOrganizationHandlerUnitTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly Mock<ICatalogDbContextFactory> _mockCatalogDbContextFactory;
    private readonly Mock<IRequestContext> _mockRequestContext;
    private readonly CreateOrganizarionHandler _createOrganizarionHandler;

    public CreateOrganizationHandlerUnitTests()
    {
        _mockOrganizationRepository = new Mock<IOrganizationRepository>();
        _mockCatalogDbContextFactory = new Mock<ICatalogDbContextFactory>();
        _mockRequestContext = new Mock<IRequestContext>();
        _createOrganizarionHandler = new CreateOrganizarionHandler(_mockOrganizationRepository.Object, _mockCatalogDbContextFactory.Object, _mockRequestContext.Object);
    }

    /// <summary>
    /// Unit test to correctly test the 'create organization' function
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task HandleCreateOrganization_IsOk()
    {
        var command = new CreateOrganizationCmd()
        {
            Name = "org",
            TenantIdentifier = "org"
        };

        var result = await _createOrganizarionHandler.Handle(command, default);

        result.IsError.Should().BeFalse();
    }
}