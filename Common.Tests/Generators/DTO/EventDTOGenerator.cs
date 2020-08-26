using Application.DTO;
using System;
using System.Collections.Generic;

namespace Common.Tests.Generators.DTO
{
    public static class EventDTOGenerator
    {
        public static UpdateEventDTO GetValidUpdateEventDTOWithInvalidEventTypeId()
        {
            return new UpdateEventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = -11
            };
        }

        public static UpdateEventDTO GetValidUpdateEventDTO()
        {
            return new UpdateEventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = 1
            };
        }

        public static CreateEventDTO GetValidCreateEventDTO()
        {
            return new CreateEventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = 1,
                CreatedDate = DateTime.Now.AddDays(-5)
            };
        }

        public static EventDTO GetValidEventDTO()
        {
            return new EventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = 1,
                CreatedDate = DateTime.Now.AddDays(-5),
                Id = 1
            };
        }

        public static EventDTO GetValidEventDTO(int id)
        {
            return new EventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = 1,
                CreatedDate = DateTime.Now.AddDays(-5),
                Id = id
            };
        }

        public static List<EventDTO> GetValidListOfEventDTOs()
        {
            List<EventDTO> dtos = new List<EventDTO>();
            for (int i = 0; i < 20; i++)
            {
                dtos.Add(new EventDTO()
                {
                    Name = "Logging {i}",
                    Cost = 5000,
                    EventTypeId = 1,
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Id = 1
                });
            }
            return dtos;
        }

        public static CreateEventDTO GetCreateEventDTO()
        {
            return new CreateEventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = 1,
                CreatedDate = DateTime.Now.AddDays(-5),
            };
        }

        public static CreateEventDTO GetCreateEventDTOWithInValidEventTypeId()
        {
            return new CreateEventDTO()
            {
                Name = "Logging",
                Cost = 5000,
                EventTypeId = -11,
                CreatedDate = DateTime.Now.AddDays(-5),
            };
        }
    }
}
