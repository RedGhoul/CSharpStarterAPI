using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.DTO;
using TemplateAPI.Models.Enities;

namespace TemplateAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _EventRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<EventsController> _Logger;
        public EventsController(IEventRepository eventRepository, IMapper mapper, ILogger<EventsController> logger)
        {
            _EventRepository = eventRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            _Logger.LogInformation($"Logging event by ID of {id}");
            var pointsEnity = await _EventRepository.GetByIdAsync(id);
            if (pointsEnity == null) return NotFound();
            var model = _Mapper.Map<EventDTO>(pointsEnity);

            return Ok(model);
        }

        [HttpGet("GroupId/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetByEntityTypeId(int id)
        {
            _Logger.LogError($"Logging event by ID of {id}");
            var pointsEnity = await _EventRepository.GetEventByGroupIdAsync(id);
            if (pointsEnity == null) return NotFound();
            return Ok(_Mapper.Map<EventDTO>(pointsEnity));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostEvent(EventDTO eventDTO)
        {
            var model = _Mapper.Map<Event>(eventDTO);
            var pointsEnity = await _EventRepository.AddEventAsync(model);
            if(pointsEnity){
                return Ok(eventDTO);
            }else
            {
                _Logger.LogError($"Event DTO could not be created {JsonConvert.SerializeObject(eventDTO)}");
                return BadRequest();
            }
        }

        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateEvent(EventDTO eventDTO)
        {
            var model = _Mapper.Map<Event>(eventDTO);
            var pointsEnity = await _EventRepository.UpdateEventAsync(model);
            if (pointsEnity)
            {
                return Ok(eventDTO);
            }
            else
            {
                _Logger.LogError($"Event DTO could not be created {JsonConvert.SerializeObject(eventDTO)}");
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (await _EventRepository.DeleteEventAsync(id))
            {
                return Ok();
            }
            else
            {
                _Logger.LogError($"Event could not be deleted by id of {id}");
                return BadRequest();
            }
        }

    }
}
