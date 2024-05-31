namespace CAMT.Application.Modules.Company.Organizations.Delete;

public record DeleteOrganizationCmd(Guid Id) : IRequest<ErrorOr<Unit>>;