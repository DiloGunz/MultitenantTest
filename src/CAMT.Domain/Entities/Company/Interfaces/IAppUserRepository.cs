namespace CAMT.Domain.Entities.Company.Interfaces;

public interface IAppUserRepository
{
    Task<List<AppUser>> GetAllAsync();
    Task<AppUser?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(AppUser user);
    void Update(AppUser user);
    void Delete(AppUser user);
    Task<AppUser?> GetByUsernameAsync(string username);  
}