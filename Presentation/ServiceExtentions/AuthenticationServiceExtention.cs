using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Presentation.ServiceExtentions
{
    public static class AuthenticationServiceExtention
    {
        public static IServiceCollection AddJWTAuth(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("KEYS")["PRIVATE_KEY"])),
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidIssuer = Configuration.GetSection("JWT")["VALID_ISSUER_ACCESS"],
                       ValidAudience = Configuration.GetSection("JWT")["VALID_AUDIENCE_ACCESS"],
                       ClockSkew = TimeSpan.Zero
                   };
               });
            return services;
        }
    }
}
