using Application.AutoMapper;
using Application.DTO;
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
    public class GetEventByIdHandlerShould
    {
        private readonly GetEventByIdHandler _sut;
        private readonly Mock<ILogger<GetEventByIdHandler>> _logger;

        public GetEventByIdHandlerShould()
        {
            #region Initialize Mocks & AutoMapper
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<GetEventByIdHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new GetEventByIdHandler(context, mapper, _logger.Object);
        }

        [Fact]
        public async void Return_EventDTO_On_Valid_EventId()
        {
            // Arange
            GetEventByIdQuery getEventByIdQuery = EventCommandQueryGenerator.GetValidEventByIdQuery();

            // Act
            EventDTO result = await _sut.Handle(getEventByIdQuery, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void Return_Null_On_InValid_EventId()
        {
            // Arange
            GetEventByIdQuery getInValidEventByIdQuery = EventCommandQueryGenerator.GetInValidEventByIdQuery();

            // Act
            EventDTO result = await _sut.Handle(getInValidEventByIdQuery, new CancellationToken());

            // Assert
            result.Should().BeNull();
        }

    }
}
