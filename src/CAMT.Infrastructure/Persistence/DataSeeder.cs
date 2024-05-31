using CAMT.Domain.Entities.Company;
using CAMT.Infrastructure.Persistence.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace CAMT.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

        var organization = new Organization()
        {
            Id = Guid.NewGuid(),
            Name = "MAIN",
            TenantIdentifier = "MAIN"
        };

        if (!context.Organizations.Any())
        {
            await context.Organizations.AddAsync(organization);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                UserName = "root",
                NormalizedUserName = "root",
                Email = "root@root.com",
                PasswordHash = "AQAAAAIAAYagAAAAEItQiIi423gyX5Ed5QJJ2jtBU9+/oGMX9E20dSO6ZybCpMeXt8WxOlH1+POF01gg5g==",
                SecurityStamp = "PGGRXNCNF355MCCF2EK4PENFKJY7D4T7",
                ConcurrencyStamp = "8cdb8341-460f-43af-bd6f-8afe3a814515",
                OrganizationId = organization.Id,
            };

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }
}