using System;

namespace TemplateAPI.Models.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Cost { get; set; }
    }
}
