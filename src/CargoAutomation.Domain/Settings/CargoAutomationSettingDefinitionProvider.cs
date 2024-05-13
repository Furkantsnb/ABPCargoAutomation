using Volo.Abp.Settings;

namespace CargoAutomation.Settings;

public class CargoAutomationSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CargoAutomationSettings.MySetting1));
    }
}
