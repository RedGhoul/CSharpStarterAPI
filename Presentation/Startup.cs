using Application;
using ExternalServices;
using ExternalServices.BackGroundJobs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Presentation.MiddlewareExtensions;
using Presentation.Policies;
using Presentation.ServiceExtentions;
using Presentation.Swagger;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistance(Configuration);
            services.AddExternalServices(Configuration);
            services.AddJWTAuth(Configuration);
            services.AddApplication();
            services.AddPolicies();
            services.AddCors();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            }).AddFluentValidation();
            
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.Seed();
            }
            app.UseExceptionHandler(logger);
            app.UseRouting();
            app.UseCors();
            app.UseAuthServiceMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.SetupSwagger(provider);
            app.SetupBackgroundJobSystem(env);
        }
    }
}
