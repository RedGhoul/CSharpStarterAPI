using Application.DTO.Verify;
using Application.Response.Verify;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.CustomMiddleware
{
    public class AuthServiceMiddleware
    {
        private const string AuthIdentity = "AuthorizationIdentity";
        private const string AuthAccess = "AuthorizationAccess";
        private const string BearerAndSpace = "Bearer ";
        private readonly RequestDelegate _next;

        public AuthServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            IHttpClientFactory clientFactory = (IHttpClientFactory)httpContext.RequestServices.GetService(typeof(IHttpClientFactory));
            IConfiguration configuration = (IConfiguration)httpContext.RequestServices.GetService(typeof(IConfiguration));

            HttpClient client = clientFactory.CreateClient("AuthServiceMiddleWare");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                configuration.GetSection("External")["API_END_POINT_AUTH_SERVICE"]);

            VerifyTokensDTO verifyDTO = new VerifyTokensDTO();

            string AuthorizationIdentity = httpContext.Request.Headers[AuthIdentity];
            string AuthorizationAccess = httpContext.Request.Headers[AuthAccess];

            if (AuthorizationIdentity == null)
            {
                return _next(httpContext);
            }

            verifyDTO.AccessTokenServiceY = AuthorizationAccess.Replace(BearerAndSpace, "");
            verifyDTO.IdentityToken = AuthorizationIdentity.Replace("Bearer ", "");

            request.Content = new StringContent(JsonConvert.SerializeObject(verifyDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (JsonConvert.DeserializeObject<VerifyResponse>(await response.Content.ReadAsStringAsync()).valid)
            {
                httpContext.Request.Headers["Authorization"] = AuthorizationAccess;
                httpContext.Request.Headers.Remove(AuthIdentity);
                httpContext.Request.Headers.Remove(AuthAccess);
                return _next(httpContext);
            }
            else
            {
                throw new Exception("Could not verify authenticity of caller");
            }
        }
    }
}
