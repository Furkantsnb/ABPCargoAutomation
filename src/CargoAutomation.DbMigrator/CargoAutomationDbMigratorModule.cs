using CargoAutomation.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CargoAutomation.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CargoAutomationEntityFrameworkCoreModule),
    typeof(CargoAutomationApplicationContractsModule)
    )]
public class CargoAutomationDbMigratorModule : AbpModule
{
}
