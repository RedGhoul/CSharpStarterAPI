using Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Events
{
    public class GetEventByEntityTypeIdQuery : IRequest<List<EventDTO>>
    {
        public int Id { get; set; }
        public GetEventByEntityTypeIdQuery(int id)
        {
            Id = id;
        }
    }
}
