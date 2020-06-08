using MediatR;
using System;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace TemplateAPI.DAL.CQRS.Commands.Events
{
    public class UpdateEventCommand : IRequest<UpdateEventResponse>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Cost { get; set; }
    }
}
