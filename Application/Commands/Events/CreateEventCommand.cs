using MediatR;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Application.Commands.Events
{
    public class CreateEventCommand : IRequest<CreateEventResponse>
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }

    }
}
