using System.Threading.Tasks;

namespace CargoAutomation.Data;

public interface ICargoAutomationDbSchemaMigrator
{
    Task MigrateAsync();
}
