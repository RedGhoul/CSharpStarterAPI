using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAPI.DAL.Commands
{
    public interface IEventCommands
    {
        string GetEvents { get; }
        string GetEventById { get; }
        string GetEventByGroupId { get; }
        string AddEvent { get; }
        string UpdateEvent { get; }
        string RemoveEvent { get; }
    }
}
