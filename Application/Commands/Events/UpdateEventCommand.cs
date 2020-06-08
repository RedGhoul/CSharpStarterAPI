using Application.Queries.Events;
using MediatR;
using System;

namespace Application.Commands.Events
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
