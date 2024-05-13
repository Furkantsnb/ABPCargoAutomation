using CargoAutomation.Samples;
using Xunit;

namespace CargoAutomation.EntityFrameworkCore.Domains;

[Collection(CargoAutomationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CargoAutomationEntityFrameworkCoreTestModule>
{

}
