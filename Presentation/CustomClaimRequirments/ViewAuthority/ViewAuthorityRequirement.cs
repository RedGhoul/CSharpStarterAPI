using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Policies
{
    public class ViewAuthorityRequirement : IAuthorizationRequirement
    {
        public ViewAuthorityRequirement(string requiredLevel)
        {
            RequiredLevel = requiredLevel;
        }

        public string RequiredLevel { get; set; }
    }
}
