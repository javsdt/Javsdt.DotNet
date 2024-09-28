using Javsdt.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<JavService>();
            services.AddScoped<SubtitleService>();
            services.AddScoped<MovieService>();
        }
    }
}
