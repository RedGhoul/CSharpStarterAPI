using MediatR;
using TemplateAPI.Models.DTO;

namespace TemplateAPI.DAL.Queries.Events
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
