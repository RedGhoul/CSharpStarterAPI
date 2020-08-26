using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.IO;
using System.Linq;

namespace Common.Tests.Integration
{
    /**
     * Thing you get when using a "real" db:
     * It will slap you if:
     * you go against a constraint
     * you don't respect a data type (trying to put in a long string into navchar(2))
     * **/
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables()
       .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseConfiguration(Configuration);
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(
                          options =>
                          {
                              options.UseSqlServer(Configuration.GetConnectionString("TEST_DB"));
                          });


                ServiceProvider sp = services.BuildServiceProvider();

                using IServiceScope scope = sp.CreateScope();
                IServiceProvider scopedServices = scope.ServiceProvider;
                ApplicationDbContext context = scopedServices.GetRequiredService<ApplicationDbContext>();
                ILogger<CustomWebApplicationFactory<TStartup>> logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                context.Database.EnsureDeleted();
            });
        }
    }
}
