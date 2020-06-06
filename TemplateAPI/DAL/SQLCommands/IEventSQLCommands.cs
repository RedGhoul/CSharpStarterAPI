namespace TemplateAPI.DAL.SQLCommands
{
    public interface IEventSQLCommands
    {
        string GetEvents { get; }
        string GetEventById { get; }
        string GetEventByGroupId { get; }
        string AddEvent { get; }
        string UpdateEvent { get; }
        string RemoveEvent { get; }
    }
}
