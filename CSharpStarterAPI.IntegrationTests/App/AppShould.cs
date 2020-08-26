using Common.Tests.Integration;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CSharpStarterAPI.IntegrationTests.App
{
    [Trait("Category", "Integration")]
    public class AppShould : IntegrationTestBase
    {
        [Fact]
        public async Task Return_NotFound_On_GetRequest_With_Invalid_URI()
        {
            // Arrange
            string malFormedURL = "api/Event/3";

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(malFormedURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Return_BadRequest_On_GetRequest_With__Malformed_URL_Invalid_API_Version()
        {
            // Arrange
            string malFormedURL = "api/Events?greatethancost=34&api-version=1.1";

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(malFormedURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
