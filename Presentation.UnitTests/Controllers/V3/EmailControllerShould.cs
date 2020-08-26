using Application.AutoMapper;
using Application.Commands;
using Application.DTO.Email;
using AutoMapper;
using Common.Tests.Generators.DTO;
using Common.Tests.Generators.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading;
using Xunit;

namespace Presentation.Controllers.V3
{
    [Trait("Category", "Controllers")]
    [Trait("Category", "Email")]
    public class EmailControllerShould
    {
        private readonly EmailController _sut; //system under test

        private readonly Mock<ILogger<EmailController>> _logger;
        private readonly Mock<IMediator> _mediator;

        public EmailControllerShould()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<EmailController>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();


            _sut = new EmailController(_logger.Object, _mediator.Object, mapper);
        }


        [Fact]
        public async void Return_BadRequest_If_Email_Is_Not_Sent()
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return EmailResponseGenerator.CreateInValidSendEmailResponse(); });

            SendEmailDTO sendEmailDTO = EmailDTOGenerator.GetValidSendEmailDTO();

            // Act
            IActionResult result = await _sut.SendEmail(sendEmailDTO);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(BadRequestResult));

            BadRequestResult NotFoundResult = (BadRequestResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Return_OkRequest_If_Email_Is_Sent()
        {
            // Arange
            _mediator
                .Setup(m => m.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(() => { return EmailResponseGenerator.CreateValidSendEmailResponse(); });

            SendEmailDTO sendEmailDTO = EmailDTOGenerator.GetValidSendEmailDTO();

            // Act
            IActionResult result = await _sut.SendEmail(sendEmailDTO);

            // Assert
            _mediator.Verify(_ => _.Send(It.IsAny<SendEmailCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeAssignableTo(typeof(OkResult));

            OkResult NotFoundResult = (OkResult)result;

            NotFoundResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

    }
}
