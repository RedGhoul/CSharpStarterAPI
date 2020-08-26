using System.Collections.Generic;

namespace Domain.Entities
{
    public class EventType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
