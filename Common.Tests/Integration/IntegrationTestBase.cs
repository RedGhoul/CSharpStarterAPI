using Presentation;
using System.Net.Http;

namespace Common.Tests.Integration
{
    public class IntegrationTestBase
    {
        public readonly string applicationJson = "application/json";
        protected readonly HttpClient TestClient;
        public IntegrationTestBase()
        {
            CustomWebApplicationFactory<Startup> appFactory = new CustomWebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }
    }
}
