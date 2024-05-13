using Xunit;

namespace CargoAutomation.EntityFrameworkCore;

[CollectionDefinition(CargoAutomationTestConsts.CollectionDefinitionName)]
public class CargoAutomationEntityFrameworkCoreCollection : ICollectionFixture<CargoAutomationEntityFrameworkCoreFixture>
{

}
