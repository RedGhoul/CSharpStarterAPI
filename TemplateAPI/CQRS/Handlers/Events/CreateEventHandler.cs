using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TemplateAPI.DAL.CQRS.Commands.Events;
using TemplateAPI.DAL.CQRS.Response.Events;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.DAL.CQRS.Handlers.Events
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, CreateEventResponse>
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<CreateEventHandler> _Logger;

        public CreateEventHandler(IEventRepository eventRepository, IMapper mapper, ILogger<CreateEventHandler> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<CreateEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _Mapper.Map<Event>(request);
            var result = await _EventRepository.AddEventAsync(@event);
            return new CreateEventResponse() { IsCreated = result };
        }
    }
}
