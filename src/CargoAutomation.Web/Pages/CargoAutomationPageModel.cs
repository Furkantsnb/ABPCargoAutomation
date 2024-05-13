using CargoAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CargoAutomation.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class CargoAutomationPageModel : AbpPageModel
{
    protected CargoAutomationPageModel()
    {
        LocalizationResourceType = typeof(CargoAutomationResource);
    }
}
