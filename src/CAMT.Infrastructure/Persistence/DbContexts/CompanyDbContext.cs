using CAMT.Domain.Data;
using CAMT.Domain.Entities.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CAMT.Infrastructure.Persistence.DbContexts;

public class CompanyDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, ICompanyDbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
    {

    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<AppUser> Users { get; set ; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}