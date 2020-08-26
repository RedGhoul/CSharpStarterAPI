using Application.DTO;
using Application.Helpers;
using Application.Queries.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Events
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<EventDTO>>
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<GetAllEventsHandler> _Logger;
        private readonly ApplicationDbContext _Context;

        public GetAllEventsHandler(ApplicationDbContext context,
            IMapper mapper, ILogger<GetAllEventsHandler> logger)
        {
            _Mapper = mapper;
            _Logger = logger;
            _Context = context;
        }

        public async Task<List<EventDTO>> Handle(GetAllEventsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return null;
            }
            _Logger.LogInformation($"Using a pagesize of : ${request.PageSize}");

            IQueryable<Event> entities = _Context.Events.AsQueryable();

            if (request.GreaterThanCost != -1)
            {
                entities = entities.Where(x => x.Cost > request.GreaterThanCost);
            }

            if (request.LessThanCost != -1)
            {
                entities = entities.Where(x => x.Cost < request.LessThanCost);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                entities = entities.Where(x => x.Name.Contains(request.Name));
            }

            return _Mapper.Map<List<EventDTO>>(
                await entities.ToPagedListAsync(
                    request.PageNumber,
                    request.PageSize
                    )
                );
        }

    }
}
