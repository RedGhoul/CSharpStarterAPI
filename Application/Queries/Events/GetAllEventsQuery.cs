using Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Events
{
    public class GetAllEventsQuery : IRequest<List<EventDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        public decimal GreaterThanCost { get; set; } = -1;
        public decimal LessThanCost { get; set; } = -1;
        public string Name { get; set; } = null;
    }
}
