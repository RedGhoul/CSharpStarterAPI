using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateAPI.DAL.Repos;

namespace TemplateAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventRepository object1;
        private IMapper object2;

        public EventsController(IEventRepository object1, IMapper object2)
        {
            this.object1 = object1;
            this.object2 = object2;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}
