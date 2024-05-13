using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace CargoAutomation.Pages;

public class Index_Tests : CargoAutomationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
