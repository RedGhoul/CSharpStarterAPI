using Dapper;
using Domain.Enities;
using Persistence.Connection;
using Persistence.Repos.SQLCommands;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public class EventRepository : IEventRepository
    {
        private readonly IConnectionFactory _ConnectionFactory;
        private readonly IEventSQLCommands _EventCommands;
        public EventRepository(IConnectionFactory connectionFactory, IEventSQLCommands eventCommands)
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

        public async Task<List<Event>> GetEventsAsync(int pageSize, int pageNumber)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.QueryAsync<Event>(_EventCommands.GetEvents,
                      new { PageSize = pageSize, PageNumber = pageNumber }, commandType: CommandType.Text);
            return result.ToList();
        }

        public async Task<List<Event>> GetEventByGroupIdAsync(int id)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.QueryAsync<Event>(_EventCommands.GetEventByEntityId,
                 new { EntityId = id }, commandType: CommandType.Text);
            return result.ToList();
        }

        public async Task<bool> AddEventAsync(Event @event)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.ExecuteAsync(_EventCommands.AddEvent,
                 new
                 {
                     Name = @event.Name,
                     Cost = @event.Cost,
                     EntityId = @event.EntityId,
                     Description = @event.Description
                 }, commandType: CommandType.Text);
            return result > 0;
        }

        public async Task<bool> UpdateEventAsync(Event @event)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.ExecuteAsync(_EventCommands.UpdateEvent,
                 new
                 {
                     Name = @event.Name,
                     Cost = @event.Cost,
                     Id = @event.Id
                 }, commandType: CommandType.Text);
            return result > 0;
        }

        public async Task<bool> DeleteEventAsync(int Id)
        {
            using IDbConnection conn = _ConnectionFactory.GetConnection();
            var result = await conn.ExecuteAsync(_EventCommands.RemoveEvent,
                 new { Id = Id }, commandType: CommandType.Text);
            return result > 0;
        }
    }
}
