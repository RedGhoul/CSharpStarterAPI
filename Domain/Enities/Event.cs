using System;

namespace Domain.Enities
{
    public class Event
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedOnDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
