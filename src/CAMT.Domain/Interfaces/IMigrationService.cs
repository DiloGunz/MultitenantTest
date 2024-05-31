namespace CAMT.Domain.Interfaces;

public interface IMigrationService
{
    void ApplyMigrations(string tenantIdentifier);
}