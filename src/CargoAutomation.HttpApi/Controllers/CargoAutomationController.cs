using CargoAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CargoAutomation.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CargoAutomationController : AbpControllerBase
{
    protected CargoAutomationController()
    {
        LocalizationResource = typeof(CargoAutomationResource);
    }
}
