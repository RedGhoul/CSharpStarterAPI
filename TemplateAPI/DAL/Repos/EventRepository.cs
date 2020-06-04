using API.Utilities.Configuration;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPI.DAL.Executers;
using TemplateAPI.DAL.Queries;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.DAL.Repos
{
    public class EventRepository : IEventRepository
    {
        private readonly ICommandEvent _commandText;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        public EventRepository(IConfiguration configuration, ICommandEvent commandText, IExecuters executers)
        {
            _commandText = commandText;
            _connStr = ConfigManager.GetConnectionString(configuration, "PrimaryConnection");
            _executers = executers;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var result = await _executers.ExecuteCommand(_connStr,
              conn => conn.QueryAsync<Event>(_commandText.GetEventById,
                  new { id }, commandType: CommandType.Text));
            return result.FirstOrDefault();
        }

        public async Task<List<Event>> GetPointsAsync(int pageSize, int pageNumber)
        {
            var result = await _executers.ExecuteCommand(_connStr,
                  conn => conn.QueryAsync<Event>(_commandText.GetEvents,
                      new { PageSize = pageSize, PageNumber = pageNumber }, commandType: CommandType.Text));
            return result.ToList();
        }

        public async Task<List<Event>> GetPointsByGroupIdAsync(int id)
        {
            var query = await _executers.ExecuteCommand(_connStr,
             conn => conn.QueryAsync<Event>(_commandText.GetEventByGroupId,
                 new { Budgetid = id }, commandType: CommandType.Text));
            return query.ToList();
        }
    }
}
