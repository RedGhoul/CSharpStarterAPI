namespace TemplateAPI.DAL.Queries
{
    public interface ICommandEvent
    {
        string GetEventById { get; }
        string GetEvents { get; }
        string AddEvent { get; }
        string GetEventByGroupId { get; }
    }
}
