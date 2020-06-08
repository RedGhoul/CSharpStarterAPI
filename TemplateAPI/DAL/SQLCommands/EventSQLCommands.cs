namespace TemplateAPI.DAL.SQLCommands
{
    public class EventSQLCommands : IEventSQLCommands
    {
        public string GetEvents => @"Select * From Event ORDER BY CreatedDate
                                    OFFSET @PageSize * (@PageNumber - 1) ROWS
                                    FETCH NEXT @PageSize ROWS ONLY";
        public string GetEventByEntityId => @"Select * From Event Where EntityId= @EntityId";
        public string GetEventById => "Select * From Event Where Id= @Id";
        public string AddEvent => "Insert Into Event (Name, Description,EntityId, Cost, CreatedDate,UpdatedOnDate) Values (@Name,@Description,@EntityId, @Cost, GETDATE(),GETDATE())";
        public string UpdateEvent => "Update Event set Name = @Name, Cost = @Cost, UpdatedOnDate =GETDATE(), CreatedDate = GETDATE() Where Id =@Id";
        public string RemoveEvent => "Delete From Event Where Id= @Id";
    }
}
