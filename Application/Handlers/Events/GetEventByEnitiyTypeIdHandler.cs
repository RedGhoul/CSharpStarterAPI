using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Events
{
    public class GetEventByEnitiyTypeIdHandler : IRequestHandler<GetEventByEventTypeIdQuery, List<EventDTO>>
    {

        private readonly IMapper _Mapper;
        private readonly ILogger<GetEventByEnitiyTypeIdHandler> _Logger;
        private readonly ApplicationDbContext _Context;

        public GetEventByEnitiyTypeIdHandler(ApplicationDbContext context,
            IMapper mapper, ILogger<GetEventByEnitiyTypeIdHandler> logger)
        {
            _Context = context;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<List<EventDTO>> Handle(GetEventByEventTypeIdQuery request,
            CancellationToken cancellationToken)
        {
            List<Event> eventEntities = await _Context.Events.
                Where(x => x.EventTypeId == request.Id).ToListAsync();
            return _Mapper.Map<List<EventDTO>>(eventEntities);
        }
    }
}
