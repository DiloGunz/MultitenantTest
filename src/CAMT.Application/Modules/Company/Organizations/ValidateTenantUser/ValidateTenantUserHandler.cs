using CAMT.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Application.Modules.Company.Organizations.ValidateTenantUser;

internal class ValidateTenantUserHandler : IRequestHandler<ValidateTenantUserCmd, ErrorOr<Unit>>
{
    private readonly ICompanyDbContext _companyDbContext;

    public ValidateTenantUserHandler(ICompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
    }

    public async Task<ErrorOr<Unit>> Handle(ValidateTenantUserCmd request, CancellationToken cancellationToken)
    {
        var user = await _companyDbContext.Users.Include(x => x.Organization).SingleOrDefaultAsync(x => x.Id == request.IdUser);

        if (user == null)
        {
            return Error.NotFound(description: "User not found");
        }

        if (user.Organization == null)
        {
            return Error.NotFound(description: "The user is not assigned to an organization");
        }

        if (user.Organization.Name.ToLower() != request.TenanIdentifier.ToLower())
        {
            return Error.Validation(description: "The name of the organization admitted does not match the one that the user has");
        }

        return Unit.Value;
    }
}