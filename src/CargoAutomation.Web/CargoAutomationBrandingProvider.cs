using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CargoAutomation.Web;

[Dependency(ReplaceServices = true)]
public class CargoAutomationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CargoAutomation";
}
