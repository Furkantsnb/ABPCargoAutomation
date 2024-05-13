using Volo.Abp.Modularity;

namespace CargoAutomation;

/* Inherit from this class for your domain layer tests. */
public abstract class CargoAutomationDomainTestBase<TStartupModule> : CargoAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
