using Application.Commands.Events;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Events
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, UpdateEventResponse>
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<UpdateEventHandler> _Logger;
        private readonly ApplicationDbContext _Context;

        public UpdateEventHandler(ApplicationDbContext context, IMapper mapper,
            ILogger<UpdateEventHandler> logger)
        {
            _Mapper = mapper;
            _Logger = logger;
            _Context = context;
        }

        public async Task<UpdateEventResponse> Handle(UpdateEventCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.Event oldEvent = await _Context.Events.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (oldEvent == null)
                {
                    return new UpdateEventResponse()
                    {
                        Success = false
                    };
                }

                _Mapper.Map(request, oldEvent);

                return new UpdateEventResponse()
                {
                    Success = await _Context.SaveChangesAsync() > 0
                };

            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error occured during update. {ex.Message}");
                _Logger.LogError($"Following UpdateEventCommand was sent {JsonConvert.SerializeObject(request, Formatting.Indented)}");
                return new UpdateEventResponse()
                {
                    Success = false
                };
            }
        }
    }
}
