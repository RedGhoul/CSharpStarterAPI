using Application.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using Xunit;

namespace Presentation.Controllers.V2
{
    [Trait("Category", "Controllers")]
    [Trait("Category", "Events")]
    public class EventsControllerShould
    {
        private readonly EventsController _sut;

        private readonly Mock<ILogger<EventsController>> _logger;

        public EventsControllerShould()
        {

            _logger = new Mock<ILogger<EventsController>>();

            _sut = new EventsController(_logger.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(800)]
        public void Return_BadRequest_On_Every_Call_To_GetById(int id)
        {
            // Arange

            // Act
            IActionResult result = _sut.GetById(id);

            // Assert
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void Return_BadRequest_OnPost_Event_With_OddCost(decimal cost)
        {
            // Arange
            EventDTO curEventDto = new EventDTO()
            {
                Cost = cost
            };

            // Act
            IActionResult result = _sut.PostEvent(curEventDto);

            // Assert

            result.Should().BeAssignableTo(typeof(BadRequestObjectResult));

            BadRequestObjectResult NotFoundResult = (BadRequestObjectResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            NotFoundResult.Value.Should().BeAssignableTo(typeof(EventDTO));

            EventDTO responseObject = (EventDTO)NotFoundResult.Value;

            responseObject.Cost.Should().Be(cost);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        public void Return_OKResult_OnPost_Event_With_EvenCost(decimal cost)
        {
            // Arange
            EventDTO curEventDto = new EventDTO()
            {
                Cost = cost
            };

            // Act
            IActionResult result = _sut.PostEvent(curEventDto);

            // Assert

            result.Should().BeAssignableTo(typeof(OkObjectResult));

            OkObjectResult OkResult = (OkObjectResult)result;

            OkResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

            OkResult.Value.Should().BeAssignableTo(typeof(EventDTO));

            EventDTO responseObject = (EventDTO)OkResult.Value;

            responseObject.Cost.Should().Be(cost);
        }

        [Fact]
        public void Return_Exception_On_PleaseFail()
        {
            // Arange

            // Act
            try
            {
                IActionResult result = _sut.PleaseFail();
            }
            catch (Exception ex)
            {
                // Assert
                ex.Message.Should().Be("Something went bad");
            }
        }

    }
}
