using CAMT.Domain.Data;
using CAMT.Domain.Entities.Company;
using Microsoft.AspNetCore.Identity;

namespace CAMT.Application.Modules.Company.AppUsers.Create;

public class CreateAppUserHandler : IRequestHandler<CreateAppUserCmd, ErrorOr<Guid>>
{
    private readonly UserManager<AppUser> _userManager;
    private ICompanyDbContext _companyDbContext;

    public CreateAppUserHandler(ICompanyDbContext companyDbContext, UserManager<AppUser> userManager)
    {
        _companyDbContext = companyDbContext ?? throw new ArgumentNullException(nameof(companyDbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateAppUserCmd request, CancellationToken cancellationToken)
    {

        using var trx = await _companyDbContext.Database.BeginTransactionAsync(cancellationToken);

        var appUser = new AppUser()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            UserName = request.Username.ToLower().Trim(),
            OrganizationId = request.OrganizationId,
        };

        var response = await _userManager.CreateAsync(appUser, request.Password);

        if (!response.Succeeded)
        {
            return Error.Validation(response.Errors.Select(x => x.Description).First());
        }
                
        await trx.CommitAsync(cancellationToken);

        return appUser.Id;
    }
}