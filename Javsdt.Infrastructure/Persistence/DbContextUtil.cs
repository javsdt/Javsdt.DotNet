using Microsoft.Extensions.Configuration;
using MovieDb.Shared.Constants;

namespace Javsdt.Infrastructure.Persistence
{
    public class DbContextUtil
    {
        public static string GetConnectString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ApplicationInfo.AppSettingsJsonPath, optional: false, reloadOnChange: true)
                .Build();
            return configuration.GetConnectionString("AppDb")!;
        }
    }
}
