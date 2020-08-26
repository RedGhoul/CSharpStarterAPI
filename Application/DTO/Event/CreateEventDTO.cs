using System;

namespace Application.DTO
{
    public class CreateEventDTO
    {
        public int EventTypeId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedOnDate { get; set; }
    }
}
