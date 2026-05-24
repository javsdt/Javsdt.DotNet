using HappreeTool.Configurations;
using HappreeTool.Utils.CommonUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Javsdt.Infrastructure.Extensions
{
    public static class LoggingExtensions
    {
        public static void UseSerilogLogging(this IServiceCollection services)
        {
            IConfiguration moduleConfiguration = ConfigurationLoader.LoadModuleConfiguration(
                AppContext.BaseDirectory,
                ProjectNamespaceUtils.GetSimpleModuleName(typeof(LoggingExtensions))
            );

            // 配置 Serilog，读取 appsettings.json 里的 Serilog 配置
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(moduleConfiguration)
                .Enrich.FromLogContext()
                .Enrich.With<SimplifiedSourceContextEnricher>() // 继续使用自定义 Enricher
                .CreateLogger();

            // 注册 Serilog 到服务容器中
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }
    }

    internal class SimplifiedSourceContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties.TryGetValue("SourceContext", out LogEventPropertyValue? sourceContext))
            {
                var sourceContextString = sourceContext.ToString().Trim('"');
                if (sourceContextString.Contains("."))
                {
                    var parts = sourceContextString.Split('.');
                    var simpleCategoryName = string.Join(".", parts.Select((part, index) => index < parts.Length - 1 ? part[0].ToString() : part));
                    var simplifiedProperty = new LogEventProperty("SourceContext", new ScalarValue(simpleCategoryName));
                    logEvent.AddOrUpdateProperty(simplifiedProperty);
                }
            }
        }
    }
}