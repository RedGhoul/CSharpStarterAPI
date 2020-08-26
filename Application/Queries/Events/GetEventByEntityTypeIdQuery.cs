using Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Events
{
    public class GetEventByEventTypeIdQuery : IRequest<List<EventDTO>>
    {
        public int Id { get; set; }
        public GetEventByEventTypeIdQuery(int id)
        {
            Id = id;
        }
    }
}
