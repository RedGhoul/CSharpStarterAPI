using Application.Commands.Events;
using Application.Queries.Events;
using System;

namespace Common.Tests.Generators.CommandQuery
{
    public static class EventCommandQueryGenerator
    {
        public static UpdateEventCommand GetInValidUpdateEventCommand()
        {
            return new UpdateEventCommand()
            {
                Id = -11,
                EventTypeId = 1,
                Name = "Socks",
                UpdatedOnDate = DateTime.UtcNow,
                Cost = 100
            };
        }

        public static UpdateEventCommand GetValidUpdateEventCommand()
        {
            return new UpdateEventCommand()
            {
                Id = 1,
                EventTypeId = 1,
                Name = "Socks",
                UpdatedOnDate = DateTime.UtcNow,
                Cost = 2000
            };
        }

        public static GetEventByIdQuery GetValidEventByIdQuery()
        {
            return new GetEventByIdQuery(1);
        }

        public static GetEventByIdQuery GetInValidEventByIdQuery()
        {
            return new GetEventByIdQuery(-100);
        }

        public static GetEventByEventTypeIdQuery GetInValidGetEventByEventTypeIdQuery()
        {
            return new GetEventByEventTypeIdQuery(-1);
        }

        public static GetEventByEventTypeIdQuery GetValidGetEventByEventTypeIdQuery()
        {
            return new GetEventByEventTypeIdQuery(1);
        }

        public static GetAllEventsQuery GetAllEventsQueryWithInvalidPaging()
        {
            return new GetAllEventsQuery()
            {
                PageNumber = -3,
                PageSize = 0
            };
        }

        public static GetAllEventsQuery GetValidGetAllEventsQueryWithAllParams()
        {
            return new GetAllEventsQuery()
            {
                PageNumber = 1,
                PageSize = 2,
                GreaterThanCost = 2000,
                LessThanCost = 4000,
                Name = "Event"
            };
        }

        public static GetAllEventsQuery GetValidGetAllEventsQueryWithNoParams()
        {
            return new GetAllEventsQuery();
        }

        public static GetAllEventsQuery GetValidGetAllEventsQueryWithOnlyGreaterThanCostParam()
        {
            return new GetAllEventsQuery()
            {
                GreaterThanCost = 2500
            };
        }

        public static GetAllEventsQuery GetValidGetAllEventsQueryWithOnlyLessThanCostParam()
        {
            return new GetAllEventsQuery()
            {
                LessThanCost = 6000
            };
        }

        public static GetAllEventsQuery GetValidGetAllEventsQueryWithOnlyNameParam()
        {
            return new GetAllEventsQuery()
            {
                Name = "Event"
            };
        }

        public static CreateEventCommand GetCreateEventCommand()
        {
            return new CreateEventCommand()
            {
                Name = "Logging",
                Description = "Event for logging work",
                Cost = 5000,
                EventTypeId = 1
            };
        }

        public static CreateEventCommand GetCreateEventCommandWithNonExistantEventTypeId()
        {
            return new CreateEventCommand()
            {
                Name = "Logging",
                Description = "Event for logging work",
                Cost = 5000,
                EventTypeId = -100
            };
        }

        public static CreateEventCommand GetCreateEventCommandWithInvalidDescription()
        {
            return new CreateEventCommand()
            {
                Name = null,
                Description = null,
                Cost = 5000,
                EventTypeId = -100,
            };
        }

        public static DeleteEventCommand GetDeleteEventCommand()
        {
            return new DeleteEventCommand()
            {
                Id = 1
            };
        }

        public static DeleteEventCommand GetDeleteEventCommandWithInvalidId()
        {
            return new DeleteEventCommand()
            {
                Id = -1
            };
        }

    }
}
