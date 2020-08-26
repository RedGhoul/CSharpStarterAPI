using Microsoft.AspNetCore.Builder;
using Presentation.CustomMiddleware;

namespace Presentation.MiddlewareExtensions
{
    public static class AuthServiceMiddlewareExtentions
    {
        public static IApplicationBuilder UseAuthServiceMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthServiceMiddleware>();
        }
    }
}
