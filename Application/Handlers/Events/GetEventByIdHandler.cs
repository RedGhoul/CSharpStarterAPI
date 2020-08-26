using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Events
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, EventDTO>
    {
        private readonly ApplicationDbContext _Context;
        private readonly IMapper _Mapper;
        private readonly ILogger<GetEventByIdHandler> _Logger;

        public GetEventByIdHandler(ApplicationDbContext context,
            IMapper mapper, ILogger<GetEventByIdHandler> logger)
        {
            _Context = context;
            _Mapper = mapper;
            _Logger = logger;
        }

        public async Task<EventDTO> Handle(GetEventByIdQuery request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Event pointsEnity = await _Context.Events
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            return _Mapper.Map<EventDTO>(pointsEnity);
        }
    }
}
