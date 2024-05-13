using CargoAutomation.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CargoAutomation.Permissions;

public class CargoAutomationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CargoAutomationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CargoAutomationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CargoAutomationResource>(name);
    }
}
