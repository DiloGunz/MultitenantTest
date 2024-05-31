using CAMT.Application.Modules.Company.AppUsers.Shared;
using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;

namespace CAMT.Application.Modules.Company.AppUsers.GetAll;

public class GetAllAppUserHandler : IRequestHandler<GetAllAppUserQuery, ErrorOr<IReadOnlyList<AppUserDto>>>
{
    private readonly IAppUserRepository _appUserRepository;

    public GetAllAppUserHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository ?? throw new ArgumentNullException(nameof(appUserRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<AppUserDto>>> Handle(GetAllAppUserQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<AppUser> listResult = await _appUserRepository.GetAllAsync();

        return listResult.Select(x => new AppUserDto()
        {
            Id = x.Id,
            Name = x.Name,
            OrganizationId = x.OrganizationId,
            UserName = x.UserName ?? ""
        }).ToList();
    }
}