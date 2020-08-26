using Application.Queries.Events;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Common.Tests.Generators.Response
{
    public static class EventResponseGenerator
    {

        public static DeleteEventResponse GetValidDeleteEventResponse()
        {
            return new DeleteEventResponse()
            {
                IsDeleted = true
            };
        }

        public static DeleteEventResponse GetInValidDeleteEventResponse()
        {
            return new DeleteEventResponse()
            {
                IsDeleted = false
            };
        }

        public static CreateEventResponse GetValidCreateEventResponse()
        {
            return new CreateEventResponse()
            {
                EventId = 20
            };
        }
    }
}
