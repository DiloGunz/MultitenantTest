using CAMT.Application.Modules.Company.Organizations.Shared;

namespace CAMT.Application.Modules.Company.Organizations.GetAll;

public record GetAllOrganizationQuery : IRequest<ErrorOr<IReadOnlyList<OrganizationDto>>>
{

}