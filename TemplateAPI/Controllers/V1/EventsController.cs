using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.DTO;
namespace TemplateAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _PointRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<EventsController> _Logger;
        public EventsController(IEventRepository pointRepository, IMapper mapper, ILogger<EventsController> logger)
        {
            _PointRepository = pointRepository;
            _Mapper = mapper;
            _Logger = logger;
        }

        [HttpGet("Id/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            _Logger.LogInformation("Logging something here LogInformation");
            var pointsEnity = await _PointRepository.GetByIdAsync(id);
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
            _Logger.LogError("Logging something here  LogError");

            var pointsEnity = await _PointRepository.GetPointsByGroupIdAsync(id);
            if (pointsEnity == null) return NotFound();
            return Ok(_Mapper.Map<EventDTO>(pointsEnity));
        }
    }
}
