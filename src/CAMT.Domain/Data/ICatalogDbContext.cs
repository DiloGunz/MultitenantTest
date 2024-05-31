using CAMT.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Domain.Data;

public interface ICatalogDbContext
{
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}