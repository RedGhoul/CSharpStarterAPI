using MediatR;
using System.Collections.Generic;
using TemplateAPI.Models.DTO;

namespace TemplateAPI.DAL.Queries.Events
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
