namespace Persistence.Repos.SQLCommands
{
    public interface IEventSQLCommands
    {
        string GetEvents { get; }
        string GetEventById { get; }
        string GetEventByEntityId { get; }
        string AddEvent { get; }
        string UpdateEvent { get; }
        string RemoveEvent { get; }
    }
}
