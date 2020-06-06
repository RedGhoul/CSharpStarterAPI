namespace TemplateAPI.DAL.SQLCommands
{
    public class EventSQLCommands : IEventSQLCommands
    {
        public string GetEvents => @"Select * From Event ORDER BY CreatedDate
                                    OFFSET @PageSize * (@PageNumber - 1) ROWS
                                    FETCH NEXT @PageSize ROWS ONLY";
        public string GetEventByGroupId => @"Select * From Event Where GroupId= @GroupId";
        public string GetEventById => "Select * From Event Where Id= @Id";
        public string AddEvent => "Insert Into Event (Name, Cost, CreatedDate) Values (@Name, @Cost, @CreatedDate)";
        public string UpdateEvent => "Update Event set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() Where Id =@Id";
        public string RemoveEvent => "Delete From Event Where Id= @Id";
    }
}
