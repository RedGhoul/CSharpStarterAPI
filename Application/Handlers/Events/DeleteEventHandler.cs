using Application.Commands.Events;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Commands.Handlers.Events
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<DeleteEventHandler> _Logger;
        private readonly ApplicationDbContext _Context;

        public DeleteEventHandler(ApplicationDbContext context,
            IMapper mapper, ILogger<DeleteEventHandler> logger)
        {
            _Mapper = mapper;
            _Logger = logger;
            _Context = context;
        }

        public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.Event @event = await _Context.Events.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (@event == null)
                {
                    _Logger.LogError($"Event with the following ID {request.Id} does not exist");
                    return new DeleteEventResponse() { IsDeleted = false };
                }
                _Context.Events.Remove(@event);
                return new DeleteEventResponse() { IsDeleted = await _Context.SaveChangesAsync() > 0 };
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Failed to delete Event with the following ID {request.Id}");
                _Logger.LogError($"{ex.StackTrace}");
                return new DeleteEventResponse() { IsDeleted = false };
            }

        }
    }
}
