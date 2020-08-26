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
    public class GetAllEventsHandlerShould
    {
        private readonly GetAllEventsHandler _sut;
        private readonly Mock<ILogger<GetAllEventsHandler>> _logger;

        public GetAllEventsHandlerShould()
        {
            #region Initialize Mocks & AutoMapper

            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<GetAllEventsHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();

            #endregion

            _sut = new GetAllEventsHandler(context, mapper, _logger.Object);
        }

        [Fact]
        public async void Return_Null_On_Invalid_PagingNumbers()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator.GetAllEventsQueryWithInvalidPaging();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void Return_List_Of_EventDTOs_On_Valid_Query_With_All_Pramas()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator.GetValidGetAllEventsQueryWithAllParams();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async void Return_List_Of_100_EventDTOs_On_Valid_Query_With_No_params()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator.GetValidGetAllEventsQueryWithNoParams();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().HaveCount(100);
        }


        [Fact]
        public async void Return_List_Of_EventDTOs_On_Valid_Query_With_GreaterThanCost_Param()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator
                .GetValidGetAllEventsQueryWithOnlyGreaterThanCostParam();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().NotBeEmpty();

            foreach (EventDTO item in result)
            {
                item.Cost.Should().BeGreaterThan(GetAllEventsQuery.GreaterThanCost);
            }
        }


        [Fact]
        public async void Return_List_Of_EventDTOs_On_Valid_Query_With_LessThanCost_Param()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator
                .GetValidGetAllEventsQueryWithOnlyLessThanCostParam();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().NotBeEmpty();

            foreach (EventDTO item in result)
            {
                item.Cost.Should().BeLessThan(GetAllEventsQuery.LessThanCost);
            }
        }

        [Fact]
        public async void Return_List_Of_EventDTOs_On_Valid_Query_With_Name_Param()
        {
            // Arange
            GetAllEventsQuery GetAllEventsQuery = EventCommandQueryGenerator
                .GetValidGetAllEventsQueryWithOnlyNameParam();

            // Act
            List<EventDTO> result = await _sut.Handle(GetAllEventsQuery, new CancellationToken());

            // Assert
            result.Should().NotBeEmpty();

            foreach (EventDTO item in result)
            {
                item.Name.Should().Contain(GetAllEventsQuery.Name);
            }
        }
    }
}
