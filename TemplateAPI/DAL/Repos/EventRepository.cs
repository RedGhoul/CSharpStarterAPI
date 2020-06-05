using API.Utilities.Configuration;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPI.DAL.Commands;
using TemplateAPI.DAL.Connection;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.DAL.Repos
{
    public class EventRepository : IEventRepository
    {
        private readonly IConnectionFactory _ConnectionFactory;
        private readonly IEventCommands _EventCommands;
        public EventRepository(IConnectionFactory connectionFactory, IEventCommands eventCommands)
        {
            _ConnectionFactory = connectionFactory;
            _EventCommands = eventCommands;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.QueryAsync<Event>(_EventCommands.GetEventById,
                new { id }, commandType: CommandType.Text);
            return result.FirstOrDefault();
        }

        public async Task<List<Event>> GetPointsAsync(int pageSize, int pageNumber)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.QueryAsync<Event>(_EventCommands.GetEvents,
                      new { PageSize = pageSize, PageNumber = pageNumber }, commandType: CommandType.Text);
            return result.ToList();
        }

        public async Task<List<Event>> GetPointsByGroupIdAsync(int id)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.QueryAsync<Event>(_EventCommands.GetEventByGroupId,
                 new { GroupId = id }, commandType: CommandType.Text);
            return result.ToList();
        }
    }
}
