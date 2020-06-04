using TemplateAPI.DAL.Queries;

namespace TemplateAPI.DAL.Commands
{
    public class CommandEvent : ICommandEvent
    {
        public string GetEventById => "Select * From Event Where Id= @Id";

        public string GetEvents => @"Select * From Event ORDER BY CreatedOnUtc
                                    OFFSET @PageSize * (@PageNumber - 1) ROWS
                                    FETCH NEXT @PageSize ROWS ONLY";

        public string AddEvent => @"INSERT INTO Event (Name, Description) VALUES (@name, @description, @dateCreated);";

        public string GetEventByGroupId => @"Select * From Event Where GroupId= @GroupId";
    }
}
