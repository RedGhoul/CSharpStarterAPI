using Domain.Enities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetEventByGroupIdAsync(int id);
        Task<List<Event>> GetEventsAsync(int pageSize, int pageNumber);
        Task<bool> AddEventAsync(Event @event);
        Task<bool> UpdateEventAsync(Event @event);
        Task<bool> DeleteEventAsync(int Id);
    }

}
