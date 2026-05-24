using HappreeTool.Configurations;
using HappreeTool.Utils.CommonUtils;
using Javsdt.Domain.Configuration;
using Javsdt.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            IConfiguration moduleConfiguration = ConfigurationLoader.LoadModuleConfiguration(
                AppContext.BaseDirectory,
                ProjectNamespaceUtils.GetSimpleModuleName(typeof(ServiceCollectionExtensions))
            );
            services.Configure<StandardSettings>(moduleConfiguration.GetSection("Standard"));

            services.AddScoped<JavService>();
            services.AddScoped<SubtitleService>();
            services.AddScoped<MovieService>();
        }
    }
}