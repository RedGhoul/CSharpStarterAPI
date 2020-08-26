using Application.Commands;
using Application.Commands.Events;
using Application.DTO;
using Application.DTO.Email;
using Application.Queries.Events;
using Application.Queries.Generic;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();

            CreateMap<Event, CreateEventCommand>().ReverseMap();

            CreateMap<Event, UpdateEventCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateEventDTO, CreateEventCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateEventDTO, UpdateEventCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EventDTO, CreateEventCommand>().ReverseMap();
            CreateMap<EventDTO, UpdateEventCommand>().ReverseMap();
            CreateMap<EventDTO, DeleteEventResponse>().ReverseMap();

            CreateMap<PaginationQuery, GetAllEventsQuery>().ReverseMap();
            CreateMap<EventSortFilterQuery, GetAllEventsQuery>().ReverseMap();

            CreateMap<SendEmailDTO, SendEmailCommand>().ReverseMap();

            CreateMap<SendEmailDTO, SendEmailCommand>().ReverseMap();
            CreateMap<SendEmailCommand, SentEmailRecord>().ReverseMap();


            AllowNullCollections = true;
        }
    }
}
