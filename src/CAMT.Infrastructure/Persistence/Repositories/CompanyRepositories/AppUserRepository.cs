using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;
using CAMT.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Persistence.Repositories.CompanyRepositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly CompanyDbContext _companyDbContext;

    public AppUserRepository(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext ?? throw new ArgumentNullException(nameof(companyDbContext));
    }

    public async Task AddAsync(AppUser user)
    {
        throw new NotImplementedException();
    }

    public void Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AppUser>> GetAllAsync()
    {
        return await _companyDbContext.Users.ToListAsync();
    }

    public Task<AppUser?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        return await _companyDbContext.Users.SingleOrDefaultAsync(x => x.UserName == username);
    }

    public void Update(AppUser user)
    {
        throw new NotImplementedException();
    }
}