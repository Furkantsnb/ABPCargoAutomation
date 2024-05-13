using CargoAutomation.Samples;
using Xunit;

namespace CargoAutomation.EntityFrameworkCore.Applications;

[Collection(CargoAutomationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CargoAutomationEntityFrameworkCoreTestModule>
{

}
