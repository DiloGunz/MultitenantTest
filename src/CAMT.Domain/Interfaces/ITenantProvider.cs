namespace CAMT.Domain.Interfaces;

public interface ITenantProvider
{
    string GetTenantIdentifier();
}