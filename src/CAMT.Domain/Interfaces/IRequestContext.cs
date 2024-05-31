namespace CAMT.Domain.Interfaces;

public interface IRequestContext
{
    string GetTenantIdentifier();
    void SetTenantIdentifier(string value);
}