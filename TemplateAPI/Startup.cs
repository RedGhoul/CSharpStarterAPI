using API.Utilities.Configuration;
using API.Utilities.Swagger;
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
using TemplateAPI.PipelineBehaviors;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IConfigManager, ConfigManager>();
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddTransient<IEventSQLCommands, EventSQLCommands>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddMediatR(typeof(Startup)); // Scan for handlers
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());

            services.AddCors();
            services.AddControllers()
                .AddFluentValidation(c => 
                c.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddApiVersioning(
                   options =>
                   {
                       options.ReportApiVersions = true;
                   });
                services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    //options.IncludeXmlComments(XmlCommentsFilePath);
                });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionHandler(x => x.Run(async context =>
            //{
            //    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            //    var exception = errorFeature.Error;
            //    if (!(exception is ValidationException validationException))
            //    {
            //        throw exception;
            //    }

            //    var errors = validationException.Errors.Select(er => new
            //    {
            //        er.ErrorMessage,
            //        er.PropertyName
            //    });
            //    var errorText = JsonConvert.SerializeObject(errors);
            //    context.Response.StatusCode = 400;
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(errorText, Encoding.UTF8);
            //}));
            app.UseRouting();
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
