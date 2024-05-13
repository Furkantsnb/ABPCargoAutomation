using Volo.Abp.Modularity;

namespace CargoAutomation;

[DependsOn(
    typeof(CargoAutomationDomainModule),
    typeof(CargoAutomationTestBaseModule)
)]
public class CargoAutomationDomainTestModule : AbpModule
{

}
