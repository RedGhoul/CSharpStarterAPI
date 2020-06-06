using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateAPI.DAL.Queries.Events;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.DTO;

namespace TemplateAPI.DAL.Handlers.Events
{
    public class GetEventByEnitiyTypeIdHandler : IRequestHandler<GetEventByEntityTypeIdQuery, List<EventDTO>>
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<GetEventByEnitiyTypeIdHandler> _Logger;

        public GetEventByEnitiyTypeIdHandler(IEventRepository eventRepository, IMapper mapper, ILogger<GetEventByEnitiyTypeIdHandler> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<List<EventDTO>> Handle(GetEventByEntityTypeIdQuery request, CancellationToken cancellationToken)
        {
            var eventEnities = await _EventRepository.GetEventByGroupIdAsync(request.Id);
            return _Mapper.Map<List<EventDTO>>(eventEnities);
        }
    }
}
