using CAMT.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CAMT.Domain.Data;

public interface ICompanyDbContext
{
    DbSet<Organization> Organizations { get; set; }
    DbSet<AppUser> Users { get; set; }

    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}