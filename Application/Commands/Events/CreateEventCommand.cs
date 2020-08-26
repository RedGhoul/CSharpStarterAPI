using MediatR;
using System;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Application.Commands.Events
{
    public class CreateEventCommand : IRequest<CreateEventResponse>
    {
        public CreateEventCommand()
        {
            UpdatedOnDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
        public int EventTypeId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedOnDate { get; set; }

    }
}
