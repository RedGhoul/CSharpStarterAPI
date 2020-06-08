using Application.Commands.Events;
using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using Domain.Enities;

namespace Application.AutoMapper
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
