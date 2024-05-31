namespace CAMT.Domain.Interfaces;

public interface IConnectionStringProvider
{
    string GetConnectionString(string tenantIdentifier);
}
