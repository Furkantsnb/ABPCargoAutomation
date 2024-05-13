using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CargoAutomation.Data;
using Volo.Abp.DependencyInjection;

namespace CargoAutomation.EntityFrameworkCore;

public class EntityFrameworkCoreCargoAutomationDbSchemaMigrator
    : ICargoAutomationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCargoAutomationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the CargoAutomationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CargoAutomationDbContext>()
            .Database
            .MigrateAsync();
    }
}
