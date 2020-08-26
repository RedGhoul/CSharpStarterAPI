using Application.DTO;
using Application.Response.Generic;
using Common.Tests.Generators.CommandQuery;
using Common.Tests.Integration;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TemplateAPI.DAL.CQRS.Response.Events;
using Xunit;

namespace CSharpStarterAPI.IntegrationTests.V3
{
    [Trait("Category", "Integration")]
    [Trait("Category", "Events")]
    public class EventsControllerV3Should : IntegrationTestBase
    {
        private const string API_VERSION = "3.0";

        [Fact]
        public async Task Returns_OkRequest_On_GetAllEvents_Call_With_All_Params()
        {
            // Arrange
            string validGetAllEventsURL = $"api/Events?GreaterThanCost=2000&LessThanCost=6000&Name=" +
                $"Event&PageNumber=1&PageSize=5&api-version={API_VERSION}";


            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(validGetAllEventsURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Returns_Valid_PagedResponse_On_GetAllEvents_Call_With_All_Params()
        {
            // Arrange
            string validGetAllEventsURL = $"api/Events?GreaterThanCost=2000&LessThanCost=6000&Name=" +
                $"Event&PageNumber=1&PageSize=5&api-version={API_VERSION}";


            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(validGetAllEventsURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string result = response.Content.ReadAsStringAsync().Result;
            PagedResponse<EventDTO> pagedResponse = JsonConvert.DeserializeObject<PagedResponse<EventDTO>>(result);
            pagedResponse.Data.Should().NotBeNull();

        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task Returns_Valid_BaseResponse_EventDTO_On_GetByIdCall(int id)
        {
            // Arrange
            string validGetGetByIdURL = $"api/Events/{id}?api-version={API_VERSION}";


            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(validGetGetByIdURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string result = response.Content.ReadAsStringAsync().Result;
            BaseResponse<EventDTO> pagedResponse = JsonConvert.DeserializeObject<BaseResponse<EventDTO>>(result);
            pagedResponse.Data.Should().NotBeNull();

        }

        [Theory]
        [InlineData(11)]
        [InlineData(33)]
        public async Task Returns_Valid_BaseResponse_List_EventDTO_On_GetByEventTypeId(int EventId)
        {
            // Arrange
            string validGetGetByIdURL = $"api/Events/EntityId/{EventId}?api-version={API_VERSION}";


            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.GetAsync(validGetGetByIdURL);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string result = response.Content.ReadAsStringAsync().Result;
            BaseResponse<List<EventDTO>> pagedResponse = JsonConvert.DeserializeObject<BaseResponse<List<EventDTO>>>(result);
            pagedResponse.Data.Should().NotBeNull();

        }

        [Fact]
        public async Task Returns_Valid_CreateEventResponse_On_Valid_CreateEventCommand()
        {
            // Arrange
            string validPostUrl = $"api/Events?api-version={API_VERSION}";
            Application.Commands.Events.CreateEventCommand createEventCommand = EventCommandQueryGenerator.GetCreateEventCommand();

            string json = JsonConvert.SerializeObject(createEventCommand);

            StringContent data = new StringContent(json, Encoding.UTF8, applicationJson);

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.PostAsync(validPostUrl, data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string result = response.Content.ReadAsStringAsync().Result;
            CreateEventResponse pagedResponse = JsonConvert.DeserializeObject<CreateEventResponse>(result);
            pagedResponse.EventId.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(13)]
        [InlineData(25)]
        public async Task Returns_OkResult_On_Update(int id)
        {
            // Arrange
            string validUpdateUrl = $"api/Events/{id}?api-version={API_VERSION}";
            Application.Commands.Events.UpdateEventCommand updateEventCommand = EventCommandQueryGenerator.GetValidUpdateEventCommand();

            string json = JsonConvert.SerializeObject(updateEventCommand);

            StringContent data = new StringContent(json, Encoding.UTF8, applicationJson);

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.PatchAsync(validUpdateUrl, data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Theory]
        [InlineData(40)]
        [InlineData(45)]
        public async Task Returns_OkResult_On_Delete(int id)
        {
            // Arrange
            string validDeleteUrl = $"api/Events/{id}?api-version={API_VERSION}";

            // Act
            System.Net.Http.HttpResponseMessage response = await TestClient.DeleteAsync(validDeleteUrl);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
