using Application.Commands.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Application.Commands.Handlers.Events
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, CreateEventResponse>
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<CreateEventHandler> _Logger;
        private readonly ApplicationDbContext _Context;
        public CreateEventHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateEventHandler> logger)
        {
            _Mapper = mapper;
            _Logger = logger;
            _Context = context;
        }

        public async Task<CreateEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                EntityEntry<Event> result = await _Context.AddAsync(_Mapper.Map<Event>(request));
                await _Context.SaveChangesAsync();
                _Logger.LogInformation($"Created a new Event with an ID of ${result.Entity.Id}");
                return new CreateEventResponse() { EventId = result.Entity.Id };
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Failed to create new Event. Message Found: ${ex.Message}");
                return null;
            }

        }
    }
}
