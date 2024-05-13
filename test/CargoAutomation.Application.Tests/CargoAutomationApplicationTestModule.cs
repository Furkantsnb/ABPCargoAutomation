using Volo.Abp.Modularity;

namespace CargoAutomation;

[DependsOn(
    typeof(CargoAutomationApplicationModule),
    typeof(CargoAutomationDomainTestModule)
)]
public class CargoAutomationApplicationTestModule : AbpModule
{

}
