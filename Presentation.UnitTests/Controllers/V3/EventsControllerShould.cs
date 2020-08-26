using Application.AutoMapper;
using Application.Commands.Events;
using Application.DTO;
using Application.Queries.Events;
using Application.Queries.Generic;
using Application.Response.Generic;
using Application.Services;
using AutoMapper;
using Common.Tests.Generators.DTO;
using Common.Tests.Generators.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using TemplateAPI.DAL.CQRS.Response.Events;
using Xunit;

namespace Presentation.Controllers.V3
{
    [Trait("Category", "Controllers")]
    [Trait("Category", "Events")]
    public class EventsControllerShould
    {
        private readonly EventsController _sut; //system under test

        private readonly Mock<ILogger<EventsController>> _logger;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IUriService> _uriService;

        public EventsControllerShould()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<EventsController>>();
            _uriService = new Mock<IUriService>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();


            _sut = new EventsController(_uriService.Object, _logger.Object, _mediator.Object, mapper);
        }


        [Fact]
        public async void Return_NotFoundResult_If_No_Events_Are_Found_When_Getting_AllEvents()
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<GetAllEventsQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return null; });

            // Act
            IActionResult result = await _sut.GetAllEvents(new EventSortFilterQuery(), new PaginationQuery());

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetAllEventsQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(NotFoundResult));

            NotFoundResult NotFoundResult = (NotFoundResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Return_OKObjectResult_If_Events_Are_Found_When_Getting_AllEvents()
        {
            // Arange
            PaginationQuery paginationQuery = new PaginationQuery();

            _mediator
                .Setup(m => m.Send(It.IsAny<GetAllEventsQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(EventDTOGenerator.GetValidListOfEventDTOs());

            _uriService.Setup(ser => ser.GetAllObjectsNextPageUri(paginationQuery))
                .Returns(() =>
                {
                    return new Uri("https://valid_url.com").ToString();
                });

            _uriService.Setup(ser => ser.GetAllObjectsNextPageUri(paginationQuery))
                .Returns(() =>
                {
                    return new Uri("https://valid_url.com").ToString();
                });

            // Act
            IActionResult result = await _sut.GetAllEvents(new EventSortFilterQuery(), paginationQuery);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetAllEventsQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(OkObjectResult));

            OkObjectResult OkResult = (OkObjectResult)result;

            OkResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

            OkResult.Value.Should().BeAssignableTo(typeof(PagedResponse<EventDTO>));

            PagedResponse<EventDTO> responseObject = (PagedResponse<EventDTO>)OkResult.Value;

            responseObject.Data.Should().NotBeNull();
        }


        [Theory]
        [InlineData(21)]
        [InlineData(13)]
        [InlineData(14)]
        public async void Return_NotFoundResult_If_Event_DoesNot_Exist_When_Getting_EventById(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return null; });

            // Act
            IActionResult result = await _sut.GetById(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(NotFoundResult));

            NotFoundResult NotFoundResult = (NotFoundResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(13)]
        [InlineData(12)]
        public async void Return_OKResult_With_EventDTO_If_Event_Exists_When_Getting_EventById(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventDTOGenerator.GetValidEventDTO());

            // Act
            IActionResult result = await _sut.GetById(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(IActionResult));
            result.Should().BeOfType(typeof(OkObjectResult));

            OkObjectResult ObjectResult = result as OkObjectResult;

            ObjectResult.Value.Should().BeOfType(typeof(BaseResponse<EventDTO>));
            ObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }


        [Theory]
        [InlineData(21)]
        [InlineData(13)]
        [InlineData(14)]
        public async void Return_NotFoundResult_If_EventType_DoesNot_Exist_When_Getting_EventByEventTypeId(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<GetEventByEventTypeIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return new List<EventDTO>(); });

            // Act
            IActionResult result = await _sut.GetByEventTypeId(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetEventByEventTypeIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(NotFoundResult));

            NotFoundResult NotFoundResult = (NotFoundResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(13)]
        [InlineData(12)]
        public async void Return_OKResult_With_EventDTO_If_EventType_Exists_When_Getting_EventByEventTypeId(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<GetEventByEventTypeIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventDTOGenerator.GetValidListOfEventDTOs());

            // Act
            IActionResult result = await _sut.GetByEventTypeId(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<GetEventByEventTypeIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(IActionResult));
            result.Should().BeOfType(typeof(OkObjectResult));

            OkObjectResult ObjectResult = result as OkObjectResult;

            ObjectResult.Value.Should().BeOfType(typeof(BaseResponse<List<EventDTO>>));
            ObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async void Return_BadResult_If_Event_DoesNot_GetCreated()
        {
            // Arange
            CreateEventDTO newCreateEventDTO = EventDTOGenerator.GetValidCreateEventDTO();

            _mediator
                .Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return null; });

            // Act
            IActionResult result = await _sut.CreateEvent(newCreateEventDTO);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(BadRequestResult));

            BadRequestResult BadRequestResult = (BadRequestResult)result;

            BadRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Return_OKResult_If_Event_Does_GetCreated()
        {
            // Arange
            CreateEventDTO newCreateEventDTO = EventDTOGenerator.GetValidCreateEventDTO();

            _mediator
                .Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventResponseGenerator.GetValidCreateEventResponse());

            // Act
            IActionResult result = await _sut.CreateEvent(newCreateEventDTO);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(IActionResult));

            result.Should().BeOfType(typeof(OkObjectResult));

            OkObjectResult ObjectResult = result as OkObjectResult;

            ObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

            ObjectResult.Value.Should().BeOfType(typeof(CreateEventResponse));

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Return_NotFoundResult_If_Event_Does_Not_Exist_When_Deleting_Event(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventResponseGenerator.GetInValidDeleteEventResponse());

            // Act
            IActionResult result = await _sut.DeleteEvent(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(IActionResult));
            result.Should().BeOfType(typeof(NotFoundResult));

            NotFoundResult ObjectResult = result as NotFoundResult;

            ObjectResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Return_OKResult_If_Event_Exists_When_Deleting_Event(int id)
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(EventResponseGenerator.GetValidDeleteEventResponse());

            // Act
            IActionResult result = await _sut.DeleteEvent(id);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(IActionResult));
            result.Should().BeOfType(typeof(OkResult));

            OkResult ObjectResult = result as OkResult;

            ObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

    }
}
