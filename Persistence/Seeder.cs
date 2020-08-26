using Bogus;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch (System.Exception)
            {

            }

            GenerateData(context);

        }
        private static void GenerateData(ApplicationDbContext context)
        {

            Faker faker = new Faker("en");

            if (context.EventTypes.Count() == 0)
            {
                List<EventType> eventTypes = new List<EventType>();

                for (int i = 0; i < 200; i++)
                {
                    eventTypes.Add(new EventType
                    {
                        Description = faker.Lorem.Sentence(faker.Random.Number(1, 20))
                    });
                }
                context.AddRange(eventTypes);
                context.SaveChanges();
            }

            if (context.Events.Count() == 0)
            {
                List<Event> events = new List<Event>();

                for (int i = 1; i < 200; i++)
                {
                    if (i > 0 && i <= 50)
                    {
                        events.Add(new Event
                        {
                            Description = faker.Lorem.Sentence(faker.Random.Number(1, 20)),
                            Name = new Faker("en").Person.UserName + " Event",
                            Cost = faker.Random.Decimal(2000, 6000),
                            UpdatedOnDate = faker.Date.Recent(),
                            CreatedDate = faker.Date.Recent(),
                            EventTypeId = 1
                        }); ;

                    }
                    else if (i > 50 && i <= 100)
                    {
                        events.Add(new Event
                        {
                            Description = faker.Lorem.Sentence(faker.Random.Number(1, 20)),
                            Name = new Faker("en").Person.UserName + " Event",
                            Cost = faker.Random.Decimal(2000, 6000),
                            UpdatedOnDate = faker.Date.Recent(),
                            CreatedDate = faker.Date.Recent(),
                            EventTypeId = 3
                        });
                    }
                    else
                    {
                        events.Add(new Event
                        {
                            Description = faker.Lorem.Sentence(faker.Random.Number(1, 20)),
                            Name = new Faker("en").Person.UserName + " Event",
                            Cost = faker.Random.Decimal(2000, 6000),
                            UpdatedOnDate = faker.Date.Recent(),
                            CreatedDate = faker.Date.Recent(),
                            EventTypeId = 5
                        });
                    }
                }

                context.AddRange(events);
                context.SaveChanges();
            }

        }
    }
}
