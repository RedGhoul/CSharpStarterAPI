using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Policies
{
    public class ViewAuthorityRequirementHandler : AuthorizationHandler<ViewAuthorityRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewAuthorityRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "ViewAuthority"))
            {
                return Task.CompletedTask;
            }

            var Authority = context.User.FindFirst(c => c.Type == "ViewAuthority").Value;

            if(Authority == requirement.RequiredLevel) { 
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
