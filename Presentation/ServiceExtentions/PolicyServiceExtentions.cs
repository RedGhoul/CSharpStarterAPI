using Application.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Policies
{
    public static class PolicyServiceExtentions
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewAuthority", policy =>
                    policy.Requirements.Add(new ViewAuthorityRequirement("Maximum")));
            });
            services.AddSingleton<IAuthorizationHandler, ViewAuthorityRequirementHandler>();

            return services;
        }
    }
}
