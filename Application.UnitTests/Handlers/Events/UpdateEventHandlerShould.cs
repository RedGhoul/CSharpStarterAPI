using Application.AutoMapper;
using Application.Commands.Events;
using Application.Handlers.Events;
using Application.Queries.Events;
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
    public class UpdateEventHandlerShould
    {
        private readonly UpdateEventHandler _sut;
        private readonly Mock<ILogger<UpdateEventHandler>> _logger;

        public UpdateEventHandlerShould()
        {
            #region Initialize Mocks & AutoMapper
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<UpdateEventHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new UpdateEventHandler(context, mapper, _logger.Object);
        }

        [Fact]
        public async void Return_UpdateEventResponse_With_True_On_Valid_EventId()
        {
            // Arange
            UpdateEventCommand validUpdateEventCommand = EventCommandQueryGenerator.GetValidUpdateEventCommand();

            // Act
            UpdateEventResponse result = await _sut.Handle(validUpdateEventCommand, new CancellationToken());

            // Assert
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void Return_UpdateEventResponse_With_False_On_Valid_EventId()
        {
            // Arange
            UpdateEventCommand validUpdateEventCommand = EventCommandQueryGenerator.GetInValidUpdateEventCommand();

            // Act
            UpdateEventResponse result = await _sut.Handle(validUpdateEventCommand, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
        }

        [Fact]
        public async void Return_Fail_On_Null_UpdateEventCommand()
        {
            // Arange
            UpdateEventCommand nullUpdateEventCommand = null;

            // Act
            UpdateEventResponse result = await _sut.Handle(nullUpdateEventCommand, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
        }
    }
}
