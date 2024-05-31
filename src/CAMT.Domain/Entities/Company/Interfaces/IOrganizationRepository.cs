namespace CAMT.Domain.Entities.Company.Interfaces;

public interface IOrganizationRepository
{
    Task<List<Organization>> GetAllAsync();
    Task<Organization?> GetByIdAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<bool> ExistNameAsync(string name);
    Task<bool> ExistTenantIdentifierAsync(string tenantIdentifier);
    Task AddAsync(Organization organization);
    Task Update(Organization organization);
    Task Delete(Organization organization);
}