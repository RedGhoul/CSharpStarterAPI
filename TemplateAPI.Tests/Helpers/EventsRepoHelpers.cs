using TemplateAPI.Models.Enities;

namespace TemplateAPI.Tests.Helpers
{
    public static class EventsRepoHelpers
    {
        public static Event GetByIdAsyncNull(int id)
        {
            return null;
        }

        public static Event GetByIdAsync(int id)
        {
            return new Event
            {
                Id = 1,
            };
        }
    }
}
