using Application.Queries.Events;
using MediatR;
using System;

namespace Application.Commands.Events
{
    public class UpdateEventCommand : IRequest<UpdateEventResponse>
    {
        public UpdateEventCommand()
        {
            UpdatedOnDate = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedOnDate { get; set; }
        public decimal Cost { get; set; }
    }
}
