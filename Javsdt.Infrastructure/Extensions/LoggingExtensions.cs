﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;

namespace Javsdt.Infrastructure.Extensions
{
    public static class LoggingExtensions
    {
        public static void UseSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {
            // 配置应用日志的 Serilog
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u4}] {SourceContext:-30} - {Message:lj}{NewLine}{Exception}")
                .WriteTo.Logger(lc => lc
                    .Enrich.With<SimplifiedSourceContextEnricher>()
                    .Filter.ByExcluding(Matching.FromSource("M.E.D.Command")) // 排除EF Core日志
                    .WriteTo.File(configuration["Logs:App:Path"]!,
                                  rollingInterval: RollingInterval.Day,
                                  outputTemplate: configuration["Logs:App:Template"]!,
                                  restrictedToMinimumLevel: LogEventLevel.Information)
                )
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File(configuration["Logs:Error:Path"]!,
                              rollingInterval: RollingInterval.Day,
                              outputTemplate: configuration["Logs:Error:Template"]!,
                              restrictedToMinimumLevel: LogEventLevel.Warning)
                )
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                    .WriteTo.File(configuration["Logs:EF:Path"]!,
                                  rollingInterval: RollingInterval.Day,
                                  outputTemplate: configuration["Logs:EF:Template"]!,
                                  restrictedToMinimumLevel: LogEventLevel.Information))
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
