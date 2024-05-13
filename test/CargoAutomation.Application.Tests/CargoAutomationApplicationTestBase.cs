using Volo.Abp.Modularity;

namespace CargoAutomation;

public abstract class CargoAutomationApplicationTestBase<TStartupModule> : CargoAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
