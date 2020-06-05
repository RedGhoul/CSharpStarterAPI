using System;
using API.Utilities.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

namespace TemplateAPI {
    public class Program {
        public static void Main (string[] args) {

            Log.Logger = new LoggerConfiguration ()
                .Enrich.FromLogContext ()
                .WriteTo.Console (new RenderedCompactJsonFormatter())
                // Logging to the ELK Stack
                //.WriteTo.Elasticsearch (new ElasticsearchSinkOptions (new Uri ($"{ConfigManager.GetConnectionString(configuration, "ES_URL")}")) {
                //    AutoRegisterTemplate = true,
                //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                //        IndexFormat = $"{ConfigManager.GetAppSettingsValue(configuration, "AppName")}" + "-{0:yyyy.MM}"
                //})
                .CreateLogger ();

            try {
                Log.Information ("Starting up");
                CreateHostBuilder (args).Build ().Run ();
            } catch (Exception ex) {
                Log.Fatal (ex, "Application start-up failed");
            } finally {
                Log.CloseAndFlush ();
            }
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .UseSerilog ()
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}
