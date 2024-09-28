using Javsdt.Domain.Repositorys;
using Javsdt.Infrastructure.Persistence;
using Javsdt.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using Javsdt.Infrastructure.Clients;
using Javsdt.Application.Interfaces;
using Javsdt.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //数据库
            services.AddDbContext<JavsdtContext>((provider, options) =>
            {
                options.UseSqlite($"Data Source={configuration.GetConnectionString("AppDb")!}");
                //options.UseLoggerFactory(provider.GetRequiredService<ILoggerFactory>()).EnableSensitiveDataLogging();
            });

            // httpClient
            services.AddHttpClient();
            services.AddHttpClient<HttpClientWrapper>(ProcessConstant.MY_SERVICE)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    // 忽略 SSL 证书错误
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                });
            services.AddScoped<HttpClientWrapper>();
            services.AddScoped<MovieDbClient>();

            //仓储服务
            services.AddScoped<IJavRepository, JavRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISubtitleRepository, SubtitleRepository>();
        }
    }
}
