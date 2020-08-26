using Application.AutoMapper;
using Application.Commands;
using Application.Handlers.Email;
using Application.Response.Email;
using AutoMapper;
using Common.Tests.Generators.CommandQuery;
using Common.Tests.Integration;
using ExternalServices.Email;
using FluentAssertions;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace Application.UnitTests.Handlers.Email
{
    [Trait("Category", "Handlers")]
    [Trait("Category", "Email")]
    public class CreateEmailHandlerShould
    {
        private readonly CreateEmailHandler _sut;
        private readonly Mock<ILogger<CreateEmailHandler>> _logger;
        private readonly Mock<ISendEmailService> _SendEmailService;
        private readonly Mock<IBackgroundJobClient> _HangFireClient;

        public CreateEmailHandlerShould()
        {
            Persistence.ApplicationDbContext context = ApplicationDBContextInMemoryFactory.Generate();

            _logger = new Mock<ILogger<CreateEmailHandler>>();

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            IMapper mapper = configuration.CreateMapper();
            _SendEmailService = new Mock<ISendEmailService>();
            _HangFireClient = new Mock<IBackgroundJobClient>();
            _HangFireClient.Setup(c => c.Create(It.IsAny<Job>(), It.IsAny<EnqueuedState>()));
            _sut = new CreateEmailHandler(context, mapper, _logger.Object, _SendEmailService.Object, _HangFireClient.Object);
        }

        [Fact]
        public async void Pass_If_Valid_CreateEventCommand()
        {
            // Arange
            SendEmailCommand createEmailCommand = EmailCommandQueryGenerator.GetValidCreateEmailCommand();

            // Act
            SendEmailResponse result = await _sut.Handle(createEmailCommand, new CancellationToken());

            // Assert

            result.Success.Should().BeTrue();
            _HangFireClient.Verify(x => x.Create(
                    It.Is<Job>(job => job.Method.Name == "SendEmail"),
                    It.IsAny<EnqueuedState>()));
        }


        [Fact]
        public async void Fail_If_Null_CreateEventCommand()
        {
            // Arange
            SendEmailCommand createEmailCommand = null;

            // Act
            SendEmailResponse result = await _sut.Handle(createEmailCommand, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
        }
    }
}
