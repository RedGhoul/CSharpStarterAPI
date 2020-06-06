using MediatR;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace TemplateAPI.DAL.CQRS.Commands.Events
{
    public class DeleteEventCommand : IRequest<DeleteEventResponse>
    {
        public int Id { get; set; }
    }
}
