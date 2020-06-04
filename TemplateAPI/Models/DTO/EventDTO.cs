using System;

namespace TemplateAPI.Models.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
