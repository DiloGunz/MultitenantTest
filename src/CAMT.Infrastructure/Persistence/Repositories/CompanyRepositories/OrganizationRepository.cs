using CAMT.Domain.Data;
using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Persistence.Repositories.CompanyRepositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ICompanyDbContext _companyDbContext;

    public OrganizationRepository(ICompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext ?? throw new ArgumentNullException(nameof(companyDbContext));
    }

    public async Task AddAsync(Organization organization)
    {
        await _companyDbContext.Organizations.AddAsync(organization);
        await _companyDbContext.SaveChangesAsync();
    }

    public async Task Delete(Organization organization)
    {
        _companyDbContext.Organizations.Remove(organization);
        await _companyDbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _companyDbContext.Organizations.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _companyDbContext.Organizations.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ExistTenantIdentifierAsync(string tenantIdentifier)
    {
        return await _companyDbContext.Organizations.AnyAsync(x => x.TenantIdentifier == tenantIdentifier);
    }

    public async Task<List<Organization>> GetAllAsync()
    {
        return await _companyDbContext.Organizations.ToListAsync();
    }

    public async Task<Organization?> GetByIdAsync(Guid id)
    {
        return await _companyDbContext.Organizations.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(Organization organization)
    {
        _companyDbContext.Organizations.Update(organization);
        await _companyDbContext.SaveChangesAsync();
    }
}