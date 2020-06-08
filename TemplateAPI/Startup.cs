using API.Utilities.Configuration;
using API.Utilities.Swagger;
using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text;
using TemplateAPI.AutoMapper;
using TemplateAPI.DAL.Connection;
using TemplateAPI.DAL.Repos;
using TemplateAPI.DAL.SQLCommands;
using TemplateAPI.Swagger;

namespace TemplateAPI
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
            // Config
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IConfigManager, ConfigManager>();

            // DAL
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddTransient<IEventSQLCommands, EventSQLCommands>();
            services.AddTransient<IEventRepository, EventRepository>();

            // MediatR
            services.AddMediatR(typeof(Startup));

            // RateLimiting
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // FluentValidation , Controllers , Cors , SuppressMapClientErrors
            services.AddCors();
            services.AddControllers().ConfigureApiBehaviorOptions(options => {
                options.SuppressMapClientErrors = true; }).AddFluentValidation(c => 
                c.RegisterValidatorsFromAssemblyContaining<Startup>());

            //Auto Mapper
            services.AddSingleton(new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());

            // OPEN API
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddApiVersioning(options =>{
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>{
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(options =>{
                options.OperationFilter<SwaggerDefaultValues>();});

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseRouting();
            app.UseCors();
            app.UseIpRateLimiting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            
        }
    }
}
