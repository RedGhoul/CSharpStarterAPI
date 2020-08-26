using Common.Tests.Generators.DTO;
using Common.Tests.Integration;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSharpStarterAPI.IntegrationTests.V2
{
    [Trait("Category", "Integration")]
    [Trait("Category", "Events")]
    public class EventsControllerV2Should : IntegrationTestBase
    {
        private const string API_VERSION = "2.0";


        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(13)]
        public async Task Returns_BadRequest_On_Every_GetById_Call(int id)
        {
            // Arrange
            string validIdURL = $"/api/Events/{id}&api-version={API_VERSION}";


            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(validIdURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Returns_EmptyResponse_If_Invalid_EventId()
        {
            // Arrange
            int invalidEventId = 0;

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync($"/api/Events/{invalidEventId}&api-version=1.1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Returns_BadResult_If_EventDTO_Has_Odd_Cost()
        {
            // Arrange
            Application.DTO.EventDTO eventDtoWithOddCost = EventDTOGenerator.GetValidEventDTO();
            eventDtoWithOddCost.Cost = 3;

            string json = JsonConvert.SerializeObject(eventDtoWithOddCost);

            StringContent data = new StringContent(json, Encoding.UTF8, applicationJson);

            // Act
            System.Net.Http.HttpResponseMessage response =
                await TestClient.PostAsync($"/api/Events?api-version={API_VERSION}", data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Returns_OkResult_If_EventDTO_Has_Odd_Cost()
        {
            // Arrange
            Application.DTO.EventDTO eventDtoWithOddCost = EventDTOGenerator.GetValidEventDTO();
            eventDtoWithOddCost.Cost = 2;

            string json = JsonConvert.SerializeObject(eventDtoWithOddCost);

            StringContent data = new StringContent(json, Encoding.UTF8, applicationJson);

            // Act
            System.Net.Http.HttpResponseMessage response =
                await TestClient.PostAsync($"/api/Events?api-version={API_VERSION}", data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
