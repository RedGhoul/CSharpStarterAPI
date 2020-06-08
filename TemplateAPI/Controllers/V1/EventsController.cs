using Application.Commands.Events;
using Application.DTO;
using Application.Queries.Events;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _Logger;
        private readonly IMediator _Mediator;
        private readonly IMapper _Mapper;

        public EventsController(ILogger<EventsController> logger, IMediator mediator, IMapper mapper)
        {
            _Logger = logger;
            _Mediator = mediator;
            _Mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        public async Task<IActionResult> GetById(int id)
        {
            _Logger.LogInformation($"Logging event by ID of {id}");
            var query = new GetEventByIdQuery(id);
            var result = await _Mediator.Send(query);
            return result == null ? (IActionResult)NotFound() : Ok(result);
        }

        [HttpGet("EntityId/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        public async Task<IActionResult> GetByEntityTypeId(int id)
        {
            _Logger.LogError($"Logging event by ID of {id}");
            var query = new GetEventByEntityTypeIdQuery(id);
            var result = await _Mediator.Send(query);
            return result == null ? BadRequest() : (IActionResult)Ok(result);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        public async Task<IActionResult> CreateEvent(CreateEventCommand CEC)
        {
            var result = await _Mediator.Send(CEC);
            return result == null ? (IActionResult)BadRequest() : Ok();
        }

        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        public async Task<IActionResult> UpdateEvent(UpdateEventCommand UEC)
        {
            var result = await _Mediator.Send(UEC);
            return result.IsCreated ? (IActionResult)Ok() : BadRequest();
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(429)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _Mediator.Send(new DeleteEventCommand() { Id = id });
            return result.IsDeleted ? (IActionResult)Ok() : BadRequest();
        }

    }
}
