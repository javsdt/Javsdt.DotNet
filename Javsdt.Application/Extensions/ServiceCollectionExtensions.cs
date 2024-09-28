using Javsdt.Application.Helper;
using Javsdt.Application.Helpers;
using Javsdt.Application.Helpers.Base;
using Javsdt.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FileAnalyzer>();

            services.AddScoped<FileExplorer>();
            services.AddScoped<FileStandarder>();

            services.AddScoped<StandardService>();
        }
    }
}
