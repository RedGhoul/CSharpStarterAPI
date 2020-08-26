using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExternalServices.Email
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger<SendEmailService> _Logger;
        private readonly ISendGridClient _SendGridClient;
        public SendEmailService(ISendGridClient sendGridClient, ILogger<SendEmailService> logger)
        {
            _Logger = logger;
            _SendGridClient = sendGridClient;
        }

        public async Task<bool> SendSimpleSingleEmail(string Recipient, string Subject, string HTMLContent,
            string PlainTextContent)
        {
            try
            {
                // have to hardcode my email - SENDGRID needs verified Sender
                EmailAddress from = new EmailAddress("avaneesab5@gmail.com", "CAbot@ca.com");
                SendGridMessage msg = MailHelper.CreateSingleEmail(from,
                    new EmailAddress(Recipient, "CAUser"), Subject, PlainTextContent, HTMLContent);
                Response response = await _SendGridClient.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    _Logger.LogError($"Failed to send email. Response Code From SENG GRID: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Failed to send email, exception occured: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}
