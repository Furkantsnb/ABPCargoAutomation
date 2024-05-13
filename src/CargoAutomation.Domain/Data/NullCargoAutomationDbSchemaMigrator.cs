using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CargoAutomation.Data;

/* This is used if database provider does't define
 * ICargoAutomationDbSchemaMigrator implementation.
 */
public class NullCargoAutomationDbSchemaMigrator : ICargoAutomationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
