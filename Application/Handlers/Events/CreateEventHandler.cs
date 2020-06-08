using Application.Commands.Events;
using AutoMapper;
using Domain.Enities;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Repos;
using System.Threading;
using System.Threading.Tasks;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Application.Commands.Handlers.Events
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
