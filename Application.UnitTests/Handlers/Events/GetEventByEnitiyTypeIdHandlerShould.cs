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
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Application.UnitTests.Handlers.Events
{
    [Trait("Category", "Handlers")]
    [Trait("Category", "Events")]
    public class GetEventByEnitiyTypeIdHandlerShould
    {
        private readonly GetEventByEnitiyTypeIdHandler _sut;
        private readonly Mock<ILogger<GetEventByEnitiyTypeIdHandler>> _logger;

        public GetEventByEnitiyTypeIdHandlerShould()
        {
            #region Initialize Mocks & AutoMapper

            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<GetEventByEnitiyTypeIdHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new GetEventByEnitiyTypeIdHandler(context, mapper, _logger.Object);
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(2000)]
        [InlineData(-1)]
        public async void Return_Null_On_Invalid_EventTypeId(int id)
        {
            // Arange
            GetEventByEventTypeIdQuery InValidGetEventByEventTypeIdQuery = EventCommandQueryGenerator.GetInValidGetEventByEventTypeIdQuery();
            InValidGetEventByEventTypeIdQuery.Id = id;

            // Act
            List<EventDTO> result = await _sut.Handle(InValidGetEventByEventTypeIdQuery, new CancellationToken());

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async void Return_List_Of_EventDTOs_On_Valid_EventTypeId()
        {
            // Arange
            GetEventByEventTypeIdQuery ValidGetEventByEventTypeIdQuery = EventCommandQueryGenerator.GetValidGetEventByEventTypeIdQuery();

            // Act
            List<EventDTO> result = await _sut.Handle(ValidGetEventByEventTypeIdQuery, new CancellationToken());

            // Assert
            result.Should().NotBeEmpty();
        }


    }
}
