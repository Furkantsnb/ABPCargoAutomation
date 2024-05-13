using Microsoft.AspNetCore.Builder;
using CargoAutomation;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<CargoAutomationWebTestModule>();

public partial class Program
{
}
