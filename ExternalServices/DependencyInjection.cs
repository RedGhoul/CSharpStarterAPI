using ExternalServices.Email;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using System;

namespace ExternalServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Communicating with other apis
            // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            // https://www.coindesk.com/coindesk-api
            services.AddHttpClient("BitCoin");
            services.AddHttpClient("AuthServiceMiddleWare");
            services.AddSingleton<IBitCoinService, BitCoinService>();

            // Background Jobs
            services.AddHangfire(hangFireConfiguration => hangFireConfiguration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString("CONNECTION_STRING_DB"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                    }));

            services.AddSingleton<IBackgroundJobClient, BackgroundJobClient>();

            // Sending Emails
            services.AddSingleton<ISendEmailService, SendEmailService>();
            services.AddSendGrid(options =>
            {
                options.ApiKey = configuration.GetSection("External")["SEND_GRID_API_KEY"];
            });

            return services;
        }
    }
}
