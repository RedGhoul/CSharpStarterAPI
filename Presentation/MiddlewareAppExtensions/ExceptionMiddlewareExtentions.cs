using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Presentation.MiddlewareExtensions
{
    public static class ExceptionMiddlewareExtentions
    {
        public static void UseExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(appError =>
           {
               appError.Run(async context =>
               {
                   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                   context.Response.ContentType = "application/json";

                   IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                   if (contextFeature != null)
                   {
                       logger.LogError($"Error occurred at {DateTime.UtcNow} caused by: {contextFeature.Error}");

                       await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                       {
                           context.Response.StatusCode,
                           Message = "Internal Server Error"
                       }).ToString());
                   }
               });
           });

        }
    }
}
