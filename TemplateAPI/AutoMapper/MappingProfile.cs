using AutoMapper;
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
        }
    }
}
