using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.DAL.Repos
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetPointsByGroupIdAsync(int id);
        Task<List<Event>> GetPointsAsync(int pageSize, int pageNumber);
    }
}
