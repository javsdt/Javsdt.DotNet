using HappreeTool.Configurations;
using Javsdt.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            IConfiguration moduleConfiguration = ConfigurationLoader.LoadModuleConfiguration(
                AppContext.BaseDirectory, "Domain", configuration);

            services.AddScoped<JavService>();
            services.AddScoped<SubtitleService>();
            services.AddScoped<MovieService>();
        }
    }
}
