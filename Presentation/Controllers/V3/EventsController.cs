using Application.Commands.Events;
using Application.DTO;
using Application.Helpers;
using Application.Queries.Events;
using Application.Queries.Generic;
using Application.Response.Generic;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateAPI.DAL.CQRS.Response.Events;

namespace Presentation.Controllers.V3
{
    [ApiVersion("3.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _Logger;
        private readonly IMediator _Mediator;
        private readonly IMapper _Mapper;
        private readonly IUriService _uriService;

        public EventsController(IUriService UriService, ILogger<EventsController> logger,
            IMediator mediator, IMapper mapper)
        {
            _Logger = logger;
            _Mediator = mediator;
            _Mapper = mapper;
            _uriService = UriService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResponse<EventDTO>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllEvents(
            [FromQuery] EventSortFilterQuery eventSortFilterQuery, [FromQuery] PaginationQuery paginationQuery)
        {

            GetAllEventsQuery query = _Mapper.Map(eventSortFilterQuery,
                _Mapper.Map<GetAllEventsQuery>(paginationQuery));

            List<EventDTO> result = await _Mediator.Send(query);

            return result == null ?
                (IActionResult)NotFound() :
                Ok(PaginationHelpers.CreatePaginatedResponse<EventDTO>
                (_uriService, paginationQuery, result));

        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<EventDTO>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            _Logger.LogInformation($"Logging event by ID of {id}");
            GetEventByIdQuery query = new GetEventByIdQuery(id);
            EventDTO result = await _Mediator.Send(query);
            return result == null ? (IActionResult)NotFound() :
                Ok(new BaseResponse<EventDTO>(result));
        }

        [HttpGet("EntityId/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<List<EventDTO>>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByEventTypeId(int id)
        {
            _Logger.LogError($"Logging event by ID of {id}");
            GetEventByEventTypeIdQuery query = new GetEventByEventTypeIdQuery(id);
            List<EventDTO> result = await _Mediator.Send(query);
            return result.Count == 0 ? NotFound() :
                (IActionResult)Ok(new BaseResponse<List<EventDTO>>(result));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateEventResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateEvent(CreateEventDTO CEDTO)
        {
            CreateEventResponse result = await _Mediator.Send(_Mapper.Map<CreateEventCommand>(CEDTO));
            return result == null ? (IActionResult)BadRequest() : Ok(result);
        }

        [HttpPatch("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateEvent(int id, CreateEventDTO CEDTO)
        {
            UpdateEventCommand UpdateEventCommand = _Mapper.Map<UpdateEventCommand>(CEDTO);
            UpdateEventCommand.Id = id;
            UpdateEventResponse result = await _Mediator.Send(UpdateEventCommand);
            return result.Success ? (IActionResult)Ok() : BadRequest();
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            DeleteEventResponse result = await _Mediator.Send(new DeleteEventCommand() { Id = id });
            return result.IsDeleted ? (IActionResult)Ok() : NotFound();
        }

    }
}
