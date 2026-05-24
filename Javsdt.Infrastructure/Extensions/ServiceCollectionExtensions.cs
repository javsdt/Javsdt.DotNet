using HappreeTool.Configurations;
using HappreeTool.Surfers;
using Javsdt.Application.Interfaces;
using Javsdt.Domain.Repositorys;
using Javsdt.Infrastructure.Clients;
using Javsdt.Infrastructure.Configurations;
using Javsdt.Infrastructure.Persistence;
using Javsdt.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Javsdt.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            IConfiguration moduleConfiguration = ConfigurationLoader.LoadModuleConfiguration(
                AppContext.BaseDirectory, "Infrastructure");
            services.Configure<AvpiSettings>(moduleConfiguration.GetSection("ThirdPartys:Avpi"));

            //数据库
            services.AddDbContext<JavsdtContext>((provider, options) =>
            {
                options.UseSqlite($"Data Source={moduleConfiguration.GetConnectionString("AppDb")!}");
                //options.UseLoggerFactory(provider.GetRequiredService<ILoggerFactory>()).EnableSensitiveDataLogging();
            });

            // httpClient
            services.AddHttpClient();
            services.AddMyApiHttpClient();
            services.AddScoped<HttpClientWrapper>();
            services.AddScoped<MovieDbClient>();

            //仓储服务
            services.AddScoped<IJavRepository, JavRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISubtitleRepository, SubtitleRepository>();
        }
    }
}
