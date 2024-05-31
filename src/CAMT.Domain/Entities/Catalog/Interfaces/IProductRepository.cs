namespace CAMT.Domain.Entities.Catalog.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Product customer);
    Task Update(Product customer);
    Task Delete(Product customer);
}