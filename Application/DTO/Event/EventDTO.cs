﻿using System;

namespace Application.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedOnDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Cost { get; set; }
    }
}
