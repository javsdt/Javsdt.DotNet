using Javsdt.Application.Services;
using Javsdt.Domain.Helpers;
using Javsdt.Domain.Helpers.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<FileAnalyzer>();

            services.AddScoped<FileExplorer>();
            services.AddScoped<FileStandarder>();

            services.AddScoped<StandardService>();
        }
    }
}
