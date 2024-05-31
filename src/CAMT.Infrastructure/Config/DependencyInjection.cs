using CAMT.Domain.Data;
using CAMT.Domain.Entities.Catalog.Interfaces;
using CAMT.Domain.Entities.Company;
using CAMT.Domain.Entities.Company.Interfaces;
using CAMT.Domain.Interfaces;
using CAMT.Domain.Primitives;
using CAMT.Domain.Utility;
using CAMT.Infrastructure.Persistence.DbContexts;
using CAMT.Infrastructure.Persistence.Repositories.CatalogRepository;
using CAMT.Infrastructure.Persistence.Repositories.CompanyRepositories;
using CAMT.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAMT.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(Constants.CompanyDbContext)));
        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(Constants.MainCatalogDbContext)));

        services.AddScoped<ICompanyDbContext>(sp => sp.GetRequiredService<CompanyDbContext>());

        services.AddSingleton<IRequestContext, RequestContext>();
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
        services.AddScoped<ICatalogDbContextFactory, CatalogDbContextFactory>();

        services.AddScoped<IMigrationService, MigrationService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        services.AddIdentityCore<AppUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        }).AddEntityFrameworkStores<CompanyDbContext>();

        return services;
    }
}
