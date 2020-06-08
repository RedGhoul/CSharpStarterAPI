using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Repos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handlers.Events
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
