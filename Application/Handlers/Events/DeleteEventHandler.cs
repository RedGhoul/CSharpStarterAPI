using Application.Commands.Events;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Repos;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Commands.Handlers.Events
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<DeleteEventHandler> _Logger;

        public DeleteEventHandler(IEventRepository eventRepository, IMapper mapper, ILogger<DeleteEventHandler> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var result = await _EventRepository.DeleteEventAsync(request.Id);
            return new DeleteEventResponse() { IsDeleted = result };
        }
    }
}
