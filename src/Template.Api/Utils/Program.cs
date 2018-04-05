using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Api.Utils
{
    public class Program
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("BASEDIR", BaseDirectory);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .UseSerilog(LoggingConfiguration)
                .UseStartup<Startup>()
                .Build();

        private static void LoggingConfiguration(WebHostBuilderContext hostingContext, LoggerConfiguration loggingConfig)
        {
            loggingConfig
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(hostingContext.Configuration);
        }

        private static void ConfigConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            var env = ctx.HostingEnvironment;
            
            config.Sources.Clear();

            config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            config.Build();
        }
    }
}
