using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;

namespace Common.Tests.Integration
{
    public static class IntegrationDataUtilities
    {

        public static void ReinitializeDbForTests(ApplicationDbContext context)
        {
            context.Events.RemoveRange(context.Events);
            context.EventTypes.RemoveRange(context.EventTypes);
            InitializeDbForTests(context);
        }

        private static void InitializeDbForTests(ApplicationDbContext context)
        {

            List<EventType> EventTypes = new List<EventType>();
            for (int i = 0; i < 10; i++)
            {
                EventTypes.Add(new EventType()
                {
                    Description = $"Event Types ${i}"
                });
            }
            context.EventTypes.AddRange(EventTypes);
            context.SaveChanges();

            List<Domain.Entities.Event> events = new List<Domain.Entities.Event>();
            for (int i = 1; i < 200; i++)
            {
                events.Add(new Domain.Entities.Event()
                {
                    EventTypeId = i,
                    Cost = 3000 * i,
                    Name = $"Event ${i}",
                    Description = $"Event ${i}",
                    UpdatedOnDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                });
            }

            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}
