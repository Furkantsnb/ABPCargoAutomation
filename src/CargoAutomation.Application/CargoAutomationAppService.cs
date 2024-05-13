using System;
using System.Collections.Generic;
using System.Text;
using CargoAutomation.Localization;
using Volo.Abp.Application.Services;

namespace CargoAutomation;

/* Inherit your application services from this class.
 */
public abstract class CargoAutomationAppService : ApplicationService
{
    protected CargoAutomationAppService()
    {
        LocalizationResource = typeof(CargoAutomationResource);
    }
}
