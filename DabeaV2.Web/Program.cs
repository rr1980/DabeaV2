using DabeaV2.Common;
using DabeaV2.DB;
using DabeaV2.Logger;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace DabeaV2.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            ILogger logger = host.Services.GetService<ILogger<Program>>();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<DataContext>();
                    var options = services.GetRequiredService<IOptions<AppSettings>>();
                    DbInitializer.Initialize(host.Services, context, options.Value);

                }

                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while starting the application.");
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((context, builder) =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);

                //builder.AddConsole();
                builder.AddDebug();
                builder.AddFile(opts =>
                {
                    context.Configuration.GetSection("Logging").GetSection("FileLoggingOptions").Bind(opts);
                });
            })
            .UseStartup<Startup>();
    }
}
