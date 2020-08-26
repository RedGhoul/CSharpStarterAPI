using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Presentation.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ViewAuthority")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _Logger;
        public EventsController(ILogger<EventsController> logger)
        {
            _Logger = logger;
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetById(int id)
        {
            _Logger.LogInformation($"Following ID is coming in event with id {id} is coming in");
            try
            {
                throw new Exception("Something went bad");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.StackTrace);
                return BadRequest();
            }

        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        [ProducesResponseType(500)]
        public IActionResult PostEvent(EventDTO eventDTO)
        {
            if (eventDTO.Cost % 2 != 0)
            {
                return BadRequest(eventDTO);
            }
            return Ok(eventDTO);
        }

        [HttpGet("PleaseFail")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Exception), 500)]
        [ProducesResponseType(500)]
        public IActionResult PleaseFail()
        {
            throw new Exception("Something went bad");

        }
    }
}
