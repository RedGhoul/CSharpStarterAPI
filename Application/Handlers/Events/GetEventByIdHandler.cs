using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handlers.Events
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDTO>
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<GetEventByIdHandler> _Logger;

        public GetEventByIdHandler(IEventRepository eventRepository, IMapper mapper, ILogger<GetEventByIdHandler> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<EventDTO> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var pointsEnity = await _EventRepository.GetByIdAsync(request.Id);
            return _Mapper.Map<EventDTO>(pointsEnity);
        }
    }
}
