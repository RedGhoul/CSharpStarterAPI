using Application.DTO;
using MediatR;

namespace Application.Queries.Events
{
    public class GetEventByIdQuery : IRequest<EventDTO>
    {
        public int Id { get; }

        public GetEventByIdQuery(int id)
        {
            Id = id;
        }

    }
}
