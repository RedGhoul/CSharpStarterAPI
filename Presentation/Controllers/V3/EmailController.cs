using Application.Commands;
using Application.DTO.Email;
using Application.Response.Email;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Presentation.Controllers.V3
{
    [ApiVersion("3.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _Logger;
        private readonly IMediator _Mediator;
        private readonly IMapper _Mapper;

        public EmailController(ILogger<EmailController> logger,
            IMediator mediator, IMapper mapper)
        {
            _Logger = logger;
            _Mediator = mediator;
            _Mapper = mapper;
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SendEmail(SendEmailDTO CED)
        {
            _Logger.LogInformation($"Sending Email to {CED.Recipient}");
            SendEmailResponse result = await _Mediator.Send(_Mapper.Map<SendEmailCommand>(CED));
            return result.Success ? Ok() : (IActionResult)BadRequest();
        }
    }
}
