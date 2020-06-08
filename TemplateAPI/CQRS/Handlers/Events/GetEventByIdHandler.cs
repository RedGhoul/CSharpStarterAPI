using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TemplateAPI.DAL.Queries.Events;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.DTO;

namespace TemplateAPI.DAL.Handlers.Events
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
