using Application.Queries.Events;
using MediatR;

namespace Application.Commands.Events
{
    public class DeleteEventCommand : IRequest<DeleteEventResponse>
    {
        public int Id { get; set; }
    }
}
