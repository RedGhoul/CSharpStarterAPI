using Application.AutoMapper;
using Application.Commands.Events;
using Application.Commands.Handlers.Events;
using AutoMapper;
using Common.Tests.Generators.CommandQuery;
using Common.Tests.Integration;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace Application.UnitTests.Handlers.Events
{
    [Trait("Category", "Handlers")]
    [Trait("Category", "Events")]
    public class CreateEventHandlerShould
    {
        private readonly CreateEventHandler _sut;
        private readonly Mock<ILogger<CreateEventHandler>> _logger;

        public CreateEventHandlerShould()
        {
            #region Initialize Mocks & AutoMapper
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<CreateEventHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new CreateEventHandler(context, mapper, _logger.Object);
        }

        [Fact]
        public async void Return_Vaild_Event_Id_OnCreation()
        {
            // Arange
            CreateEventCommand createEventCommand = EventCommandQueryGenerator.GetCreateEventCommand();

            // Act
            TemplateAPI.DAL.CQRS.Response.Events.CreateEventResponse result = await _sut.Handle(createEventCommand, new CancellationToken());

            // Assert
            result.EventId.Should().BeGreaterThan(0);
        }


        [Fact]
        public async void Return_Null_On_NullCommand_Input()
        {
            // Arange
            CreateEventCommand createEventCommand = null;

            // Act
            TemplateAPI.DAL.CQRS.Response.Events.CreateEventResponse result = await _sut.Handle(createEventCommand, new CancellationToken());

            // Assert
            result.Should().BeNull();
        }
    }
}
