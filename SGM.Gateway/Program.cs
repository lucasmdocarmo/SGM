using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SGM.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebHost.CreateDefaultBuilder(args);

            //builder.ConfigureServices(s => s.AddSingleton(builder))
            //       .ConfigureAppConfiguration(ic => ic.AddJsonFile(Path.Combine("configuration","configuration.json")))
            //       .UseStartup<Startup>().UseIISIntegration();

            CreateHostBuilder(args).Build().Run();

            //Host.CreateDefaultBuilder(args)
            //     .ConfigureAppConfiguration((hostingContext, config) =>
            //     {
            //         config.AddJsonFile("ocelot.json");

            //     }).ConfigureWebHostDefaults(webBuilder =>
            //     {
            //         webBuilder.UseIISIntegration();
            //        webBuilder.UseStartup<Startup>();

            //    }).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  config
                      .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
                      .AddEnvironmentVariables();
              })
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              });
    }
}
