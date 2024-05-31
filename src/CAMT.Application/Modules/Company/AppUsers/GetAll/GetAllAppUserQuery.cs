using CAMT.Application.Modules.Company.AppUsers.Shared;

namespace CAMT.Application.Modules.Company.AppUsers.GetAll;

public record GetAllAppUserQuery : IRequest<ErrorOr<IReadOnlyList<AppUserDto>>>
{

}