// See https://aka.ms/new-console-template for more information

using Javsdt.Application.Extensions;
using Javsdt.Application.Services;
using Javsdt.Domain.Configuration;
using Javsdt.Domain.Extensions;
using Javsdt.Infrastructure.Extensions;
using Javsdt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    [STAThread]
    private async static Task Main(string[] args)
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        
        string[] cars = ["HODV-20467", "RCT-993", "avop-127", "SNIS-459"];
        string car = cars[3];
        // string dir = @"C:\Mine\MyJava\测试数据\测试jav";
        string dir = @"Y:\115";
        // string dir = @"Y:\单独测试2";
        // string dir = @"\\5600g-java\z\115";

        var serviceProvider = ConfigureServices();

        using (var scope = serviceProvider.CreateScope())
        {
            // var db = scope.ServiceProvider.GetRequiredService<JavsdtContext>();
            // db.Database.Migrate();
            
            StandardService standardService = scope.ServiceProvider.GetRequiredService<StandardService>();
            await standardService.Do(dir);
        }

        Console.WriteLine("Hello, World!");
    }

    private static ServiceProvider ConfigureServices()
    {
        // 静态配置
        SettingsHolder.InitializeStandard();

        var services = new ServiceCollection();
        services.UseSerilogLogging();
        services.AddDomain();
        services.AddApplication();
        services.AddInfrastructure();

        return services.BuildServiceProvider();
    }
}