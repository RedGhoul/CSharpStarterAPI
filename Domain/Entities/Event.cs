using System;

namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int? EventTypeId { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedOnDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public EventType EventType { get; set; }
    }
}
