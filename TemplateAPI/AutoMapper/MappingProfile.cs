using AutoMapper;
using TemplateAPI.DAL.CQRS.Commands.Events;
using TemplateAPI.DAL.CQRS.Response.Events;
using TemplateAPI.Models.DTO;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
            CreateMap<EventDTO, CreateEventCommand>();
            CreateMap<CreateEventCommand, EventDTO>();
            CreateMap<Event, CreateEventCommand>();
            CreateMap<CreateEventCommand, Event>();
            CreateMap<EventDTO, UpdateEventCommand>();
            CreateMap<UpdateEventCommand, EventDTO>();
            CreateMap<EventDTO, DeleteEventResponse>();
            CreateMap<DeleteEventResponse, EventDTO>();
            AllowNullCollections = true;
        }
    }
}
