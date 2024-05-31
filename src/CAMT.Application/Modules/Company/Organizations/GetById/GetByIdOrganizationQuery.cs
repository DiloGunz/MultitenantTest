using CAMT.Application.Modules.Company.Organizations.Shared;

namespace CAMT.Application.Modules.Company.Organizations.GetById;

public record GetByIdOrganizationQuery(Guid Id) : IRequest<ErrorOr<OrganizationDto>>;