using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ExternalServices.BackGroundJobs
{
    public static class BackGroundJobSetup
    {
        public static IApplicationBuilder SetupBackgroundJobSystem(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new HangFireAuthorizationFilter() }
                });
            }


            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 2,
            });

            return app;
        }
    }
}
