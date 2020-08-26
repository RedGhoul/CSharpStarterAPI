using ExternalServices.Email;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;
using Xunit;
namespace ExternalServices.UnitTests.Email
{
    [Trait("Category", "ExternalServices")]
    [Trait("Category", "ExternalAPIs")]
    public class SendEmailServiceShould
    {
        private readonly SendEmailService _sut;
        private readonly Mock<ILogger<SendEmailService>> _logger;
        private readonly Mock<ISendGridClient> _sendGridClient;

        public SendEmailServiceShould()
        {
            _logger = new Mock<ILogger<SendEmailService>>();
            _sendGridClient = new Mock<ISendGridClient>();
            _sut = new SendEmailService(_sendGridClient.Object, _logger.Object);
        }

        [Fact]
        public async void Return_True_On_Created_StatusCode()
        {
            // Arrange
            _sendGridClient.Setup(s => s.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => { return new Response(System.Net.HttpStatusCode.Created, null, null); });

            // Act
            bool result = await _sut.SendSimpleSingleEmail("joe@gmail.com", "subject", "html", "plaintext");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void Return_False_On_Non_Created_StatusCode()
        {
            // Arrange
            _sendGridClient.Setup(s => s.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => { return new Response(System.Net.HttpStatusCode.BadRequest, null, null); });

            // Act
            bool result = await _sut.SendSimpleSingleEmail("joe@gmail.com", "subject", "html", "plaintext");

            // Assert
            result.Should().BeFalse();
        }
    }
}
