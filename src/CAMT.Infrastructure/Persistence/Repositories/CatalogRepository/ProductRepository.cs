using CAMT.Domain.Data;
using CAMT.Domain.Entities.Catalog;
using CAMT.Domain.Entities.Catalog.Interfaces;
using CAMT.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Persistence.Repositories.CatalogRepository;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogDbContext _catalogDbContext;

    public ProductRepository(ICatalogDbContextFactory catalogDbContextFactory)
    {
        _catalogDbContext = catalogDbContextFactory.CreateDbContext() ?? throw new ArgumentNullException(nameof(catalogDbContextFactory));
    }

    public async Task AddAsync(Product product)
    {
        await _catalogDbContext.Products.AddAsync(product);
        await _catalogDbContext.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _catalogDbContext.Products.Remove(product);
        await _catalogDbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _catalogDbContext.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _catalogDbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _catalogDbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(Product customer)
    {
        _catalogDbContext.Products.Update(customer);
        await _catalogDbContext.SaveChangesAsync();
    }
}