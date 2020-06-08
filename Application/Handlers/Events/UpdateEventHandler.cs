using Application.Commands.Events;
using Application.Queries.Events;
using AutoMapper;
using Domain.Enities;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Repos;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handlers.Events
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, UpdateEventResponse>
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<UpdateEventHandler> _Logger;

        public UpdateEventHandler(IEventRepository eventRepository, IMapper mapper, ILogger<UpdateEventHandler> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<UpdateEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _Mapper.Map<Event>(request);
            var didUpdate = await _EventRepository.UpdateEventAsync(@event);
            return new UpdateEventResponse() { IsCreated = didUpdate };
        }
    }
}
