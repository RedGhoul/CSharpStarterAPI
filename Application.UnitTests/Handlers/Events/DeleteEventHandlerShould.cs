using Application.AutoMapper;
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
    public class DeleteEventHandlerShould
    {
        private readonly DeleteEventHandler _sut;
        private readonly Mock<ILogger<DeleteEventHandler>> _logger;

        public DeleteEventHandlerShould()
        {
            #region Initialize Mocks & AutoMapper

            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<DeleteEventHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new DeleteEventHandler(context, mapper, _logger.Object);
        }

        [Fact]
        public async void Return_True_On_Deletion()
        {
            // Arange
            Commands.Events.DeleteEventCommand deleteEventCommand = EventCommandQueryGenerator.GetDeleteEventCommand();

            // Act
            Queries.Events.DeleteEventResponse result = await _sut.Handle(deleteEventCommand, new CancellationToken());

            // Assert
            result.IsDeleted.Should().BeTrue();
        }


        [Fact]
        public async void Return_False_On_Invalid_Deletion()
        {
            // Arange
            Commands.Events.DeleteEventCommand deleteEventCommand = EventCommandQueryGenerator.GetDeleteEventCommandWithInvalidId();

            // Act
            Queries.Events.DeleteEventResponse result = await _sut.Handle(deleteEventCommand, new CancellationToken());

            // Assert
            result.IsDeleted.Should().BeFalse();
        }

    }
}
