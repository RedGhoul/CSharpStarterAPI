using System;

namespace TemplateAPI.Models.Enities
{
    public class Event
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
